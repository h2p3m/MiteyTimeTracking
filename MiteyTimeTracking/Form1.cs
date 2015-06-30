using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MiteyTimeTracking.Core;
using MiteyTimeTracking.DAL;
using MiteyTimeTracking.Properties;

namespace MiteyTimeTracking
{
	public enum TagType
	{
		Customer,
		Project,
		Service,
		Task
	}

	public enum PatternGroups
	{
		Date,
		Time,
		Customer,
		Project,
		Service,
		Task
	}

	public partial class Form1 : Form
	{
		public string ActiveCustomer { set; get; }

		private Controller Controller { get { return _controller; } }

		private TextProcessor TextProcessor { get; set; }

		public DateTime today = DateTime.Now;

		private bool _menuLock;
		private readonly MiteApiAbscrator _mcm;
		private readonly TrelloApiAbstractor _tcm;
		private readonly ListBox _tagBox;
		private readonly string _customerTagSign;
		private readonly string _tagPattern;
		private Dictionary<string, string> _allFoundTags;
		private bool _dontDetect;
		private readonly Controller _controller;

		public Form1()
		{
			ActiveCustomer = string.Empty;
			_tagPattern = string.Empty;
			const string taskTagSign = "#";
			const string serviceTagSign = "'";
			const string projectTagSign = ">";
			_customerTagSign = "<";
			_tagBox = new ListBox();
			_menuLock = false;
			InitializeComponent();

			try
			{
				_mcm = new MiteApiAbscrator();
				_controller = new Controller(this);
				TextProcessor = new TextProcessor(this);
			}
			catch (Exception ex)
			{
				MessageBox.Show(@"Es gab ein Problem mit der Mite-API:\n\n" + ex.Message + ex.InnerException, @"Fehler",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			string trelloApiKey = Settings.Default.trelloAPIKey;
			string trelloToken = Settings.Default.trelloToken;

			try
			{
				_tcm = new TrelloApiAbstractor(trelloApiKey, trelloToken);
			}
			catch (Exception ex)
			{
				MessageBox.Show(@"Es gab ein Problem mit der Trello-API:\n\n" + ex.Message + ex.InnerException, @"Fehler",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			_tagPattern = string.Format("[{0}{1}{2}{3}]"
				, _customerTagSign
				, projectTagSign
				, serviceTagSign
				, taskTagSign);

			KeyPreview = true;

			splitContainer1.Panel1Collapsed = true;
			splitContainer2.Panel2Collapsed = true;
			splitContainer1.SplitterDistance = 340;

			Controller.InitalizeMiteDay();
			Controller.SetTrelloTodo();

			_tagBox.ScrollAlwaysVisible = false;
			_tagBox.IntegralHeight = true;
			_tagBox.ItemHeight = RichTextBox1.Font.Height;
			_tagBox.Size = new Size(400, RichTextBox1.Font.Height);
			_tagBox.KeyDown += Tagbox_KeyDown;
			_tagBox.MouseDoubleClick += TagBoxOnMouseDoubleClick;
		}

		#region EventHandlers

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Menu && _menuLock == false)
			{
				MenuStripShowHide();
				_menuLock = false;
				e.Handled = true;
			}
			if (e.KeyCode == Keys.Menu && _menuLock)
			{
				_menuLock = false;
				e.Handled = true;
			}
		}

		private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Back && !string.IsNullOrWhiteSpace(RichTextBox1.Text))
			{
				if (IsTagIdentifier(RichTextBox1.Text[RichTextBox1.SelectionStart - 1].ToString()))
				{
					CancelTagDetection();
				}
			}

			//navigate through tagBox
			else if (_tagBox.Items.Count > 0
					 && (e.KeyData == Keys.Up || e.KeyData == Keys.Down))
			{
				if (e.KeyData == Keys.Up && _tagBox.SelectedIndex > 0)
				{
					_tagBox.SelectedIndex--;
				}
				else if (e.KeyData == Keys.Down && _tagBox.SelectedIndex < _tagBox.Items.Count - 1)
				{
					_tagBox.SelectedIndex++;
				}

				if (e.KeyData == Keys.Up || e.KeyData == Keys.Down)
				{
					e.Handled = true;
				}
			}

			//	//cancel tagDetection
			else if (e.KeyData == Keys.Escape)
			{
				CancelTagDetection();
			}
			else
			{
				TryApplyTagBoxSelection(e);
			}

			if (e.KeyCode == Keys.Back)
			{
				_dontDetect = true;
			}
			else
			{
				_dontDetect = false;
			}
		}

		private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((ModifierKeys & Keys.Control) == Keys.Control
				&& e.KeyChar.ToString() == "\n")
			{
				//TODO add new TimeEntryObject
				PrintStartingTime("\n");
			}
			else if ((ModifierKeys & Keys.Control) == Keys.Control
					 && e.KeyChar.ToString() == " ")
			{
				//_tagDetectionActive = true;
				//_dontDetect = false;

				CancelTagDetection();
				string word = GetWordAtTextCursor(false);
				if (!_dontDetect)
					TextProcessor.ParseWord(word);
				RichTextBox1.SelectionColor = Color.Black;

				e.Handled = true;
			}
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			CancelTagDetection();
			string word = GetWordAtTextCursor(false);
			if (!_dontDetect)
				TextProcessor.ParseWord(word);
			RichTextBox1.SelectionColor = Color.Black;
		}

		private void Tagbox_KeyDown(object sender, KeyEventArgs e)
		{
			TryApplyTagBoxSelection(e);
		}

		private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		protected override void OnShown(EventArgs e)
		{
			RichTextBox1.Focus();
			base.OnShown(e);
		}

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			RichTextBox1.Text = monthCalendar1.SelectionEnd.ToShortDateString();
		}

		private void showOrHideMenuToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_menuLock = true;
			MenuStripShowHide();
		}

		private void showOrHideLeftPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_menuLock = true;
			PanelLShowHide();
		}

		private void showOrHideRightPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_menuLock = true;
			PanelRShowHide();
		}

		private void TagBoxOnMouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
		{
			if (_tagBox.SelectedItems.Count == 1)
				ApplyTagNameToRichTextBox();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			PanelLShowHide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			MenuStripShowHide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			PanelRShowHide();
		}

		#endregion

		#region Logic

		private void PrintStartingTime(string devider)
		{
			var date = today.ToShortTimeString();
			int selectionStart = RichTextBox1.SelectionStart - 1;

			RichTextBox1.Select(selectionStart, 1);
			RichTextBox1.SelectedText = ("\n" + date + " - ");

			RichTextBox1.SelectionStart = selectionStart;
			RichTextBox1.SelectionLength = date.Length + 1;

			RichTextBox1.SelectionColor = Color.RoyalBlue;
			RichTextBox1.SelectionStart += date.Length + 4;
			RichTextBox1.SelectionColor = Color.Black;
			RichTextBox1.SelectedText = ("\n");
		}

		private void TryApplyTagBoxSelection(KeyEventArgs e)
		{
			if (_tagBox.SelectedItem != null
				&& _tagBox.SelectedItems.Count == 1
				&& (e.KeyData == Keys.Enter
					|| e.KeyData == Keys.Tab
					|| e.KeyData == Keys.Space
					|| e.KeyData == Keys.OemPeriod))
			{
				ApplyTagNameToRichTextBox();
				e.Handled = true;
			}
		}

		public void CancelTagDetection()
		{
			_tagBox.DataSource = null;
			RichTextBox1.Controls.Clear();
		}

		private string GetWordAtTextCursor(bool cutTagIndicator)
		{
			string charsAfterTextCursor = GetCharsAfterCursor();
			string charsBeforeTextCursor = GetCharsBeforeCursor();

			string wordAtCursor = charsBeforeTextCursor + charsAfterTextCursor;

			if (string.IsNullOrWhiteSpace(wordAtCursor))
				return string.Empty;
			if (cutTagIndicator && IsTagIdentifier(
				wordAtCursor.ToCharArray()[0].ToString()))
				return (wordAtCursor).Remove(0, 1);
			return wordAtCursor;
		}

		private string GetCharsBeforeCursor()
		{
			if (!string.IsNullOrWhiteSpace(RichTextBox1.Text))
			{
				int i = RichTextBox1.SelectionStart - 1;
				string charsBeforeCursor = string.Empty;
				while (i >= 0 && !string.IsNullOrWhiteSpace(RichTextBox1.Text[i].ToString()))
				{
					charsBeforeCursor += RichTextBox1.Text[i];
					if (IsTagIdentifier(charsBeforeCursor))
					{
						return ReverseCharsBeforeCursor(charsBeforeCursor);
					}
					i--;
				}
				return ReverseCharsBeforeCursor(charsBeforeCursor);
			}
			else
			{
				return string.Empty;
			}
		}

		private string GetCharsAfterCursor()
		{
			int i = RichTextBox1.SelectionStart;
			string charsAfterCursor = string.Empty;
			while (RichTextBox1.TextLength > i
				&& !string.IsNullOrWhiteSpace(RichTextBox1.Text[i].ToString())
				&& !IsTagIdentifier(RichTextBox1.Text[i].ToString()))
			{
				charsAfterCursor += RichTextBox1.Text[i];
				i++;
			}
			return charsAfterCursor;
		}

		private string ReverseCharsBeforeCursor(string charsBeforeCursor)
		{
			char[] charArray = charsBeforeCursor.ToCharArray();
			Array.Reverse(charArray);
			charsBeforeCursor = new string(charArray);
			return charsBeforeCursor;
		}

		public bool IsTagIdentifier(string e)
		{
			return Regex.Match(e, _tagPattern).Success;
		}

		private void ApplyTagNameToRichTextBox()
		{
			if (_tagBox.SelectedItem != null)
			{
				int selectionStart = RichTextBox1.SelectionStart;

				for (int i = selectionStart - 1; i > 0; i--)
				{
					if (IsTagIdentifier(RichTextBox1.Text[i].ToString()))
					{
						if (RichTextBox1.Text[i].ToString() == _customerTagSign)
						{
							//tagSign = richTextBox1.Text[i].ToString();
							ActiveCustomer = _tagBox.SelectedItem.ToString()
								.Remove(0, 1).Split(',')[0];
						}
						break;
					}
					selectionStart--;
				}

				RichTextBox1.SelectionStart = selectionStart;
				RichTextBox1.SelectionLength = GetWordAtTextCursor(true).Length;
				RichTextBox1.SelectedText = _tagBox.SelectedItem.ToString()
								.Remove(0, 1).Split(',')[0];

				_tagBox.DataSource = null;
				RichTextBox1.Controls.Clear();
			}
		}

		public void FillAndPlaceListBox(int tokenPosition, string tagName, TagType currentTagType)
		{
			//int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);

			_tagBox.DataSource = null;
			//_tagBox.Items.AddRange(GetAllSuitableTags(tagName, CurrentTagType)
			//	.Select(k => k.Key != null ? k.Key : null).ToArray());

			_allFoundTags = GetAllSuitableTags(tagName, currentTagType);
			_tagBox.DataSource = new BindingSource(_allFoundTags, null);
			_tagBox.DisplayMember = "Value";
			_tagBox.ValueMember = "Key";

			//if (_tagBox.Items.Count == 0)
			if (_allFoundTags == null || _allFoundTags.Count == 0)
			{
				RichTextBox1.Controls.Clear();
				_tagBox.DataSource = null;
				return;
			}
			int height = (RichTextBox1.Font.Height + 10) * _allFoundTags.Count;
			int width = 423;
			Size s = new Size();
			s.Height = height;
			s.Width = width;
			_tagBox.Size = s;
			RichTextBox1.Controls.Add(_tagBox);
			Point point = RichTextBox1.GetPositionFromCharIndex(tokenPosition);
			//point.Y += richTextBox1.Font.Height * (lineNumber + 1);
			point.Y += (int)Math.Ceiling(RichTextBox1.Font.GetHeight()) + 2;
			point.X += 2;
			_tagBox.Location = point;
			_tagBox.SelectedIndex = 0;
		}

		private Dictionary<string, string> GetAllSuitableTags(string tagName, TagType currenTagType)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			switch (currenTagType)
			{
				case TagType.Customer:
					result = _mcm.Customers.GetCustomerNames(tagName);
					break;
				case TagType.Project:
					result = _mcm.Projects.GetMatchedProjectNames(tagName, ActiveCustomer);
					break;
				case TagType.Service:
					result = _mcm.Services.GetMachedServiceNames(tagName)
						.Reverse().ToDictionary(d => d.Key, d => d.Value);
					break;
				case TagType.Task:
					result = _tcm.Cards.GetCardsByNumber(tagName);
					break;
			}
			return result;
		}

		#region PanelControls

		private void MenuStripShowHide()
		{
			menuStrip1.Visible = !menuStrip1.Visible;
		}

		private void PanelLShowHide()
		{
			splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
		}

		private void PanelRShowHide()
		{
			splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;

			if (splitContainer2.Panel2Collapsed)
			{
				RichTextBox1.Focus();
			}
			else
			{
				RichTextBox2.Focus();
			}
		}

		#endregion

		#endregion

		#region TextFunctions

		private void Undo()
		{
			RichTextBox1.Undo();
		}

		private void Redo()
		{
			RichTextBox1.Redo();
		}

		private void Cut()
		{
			RichTextBox1.Cut();
		}

		private void Copy()
		{
			RichTextBox1.Copy();
		}

		private void Paste()
		{
			RichTextBox1.Paste();
		}

		private void SelectAll()
		{
			RichTextBox1.SelectAll();
		}

		#endregion

		#region TextFunctionEventHandler

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Copy();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Redo();
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Cut();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Paste();
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		#endregion

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
			Controller.SetTrelloTodo();
		}
	}
}
