using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MiteyTimeTracking.DAL;

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
		date,
		time,
		customer,
		project,
		service,
		task,
		text
	}

	public partial class Form1 : Form
	{
		private bool _menuLock;
		private string _tagName;
		private bool _tagDetectionActive;
		private readonly MiteConnectorModel _mcm;
		private readonly Dictionary<String, TagType> _tagIdentifier;
		private TagType _currentTagType;
		private readonly ListBox _tagBox;
		private readonly string _customerTagSign;
		private readonly string _projectTagSign;
		private readonly string _serviceTagSign;
		private readonly string _taskTagSign;
		private readonly string _tagPattern;
		private string _line;
		private int _columnNumber;
		private string _activeCustomer;

		public Form1()
		{
			_activeCustomer = string.Empty;
			_columnNumber = 0;
			_line = string.Empty;
			_tagPattern = string.Empty;
			_taskTagSign = "#";
			_serviceTagSign = "'";
			_projectTagSign = ">";
			_customerTagSign = "<";
			_tagBox = new ListBox();
			_currentTagType = TagType.Customer;
			_tagIdentifier = new Dictionary<string, TagType>();
			_tagName = string.Empty;
			_menuLock = false;
			InitializeComponent();

			try
			{
				_mcm = new MiteConnectorModel();
			}
			catch (Exception ex)
			{
				MessageBox.Show(@"Es gab ein Problem mit der Mite-API:\n\n" + ex.Message, @"Fehler",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			_tagPattern = string.Format("[{0}{1}{2}{3}]"
				, _customerTagSign
				, _projectTagSign
				, _serviceTagSign
				, _taskTagSign);

			_tagIdentifier.Add(_customerTagSign, TagType.Customer);
			_tagIdentifier.Add(_projectTagSign, TagType.Project);
			_tagIdentifier.Add(_serviceTagSign, TagType.Service);
			_tagIdentifier.Add(_taskTagSign, TagType.Task);

			KeyPreview = true;

			splitContainer1.Panel1Collapsed = true;
			splitContainer2.Panel2Collapsed = true;
			splitContainer1.SplitterDistance = 340;

			richTextBox1.Text = DateTime.Now.ToShortDateString() + @" ";
			richTextBox1.Find(DateTime.Now.ToShortDateString());
			richTextBox1.SelectionColor = Color.Sienna;
			richTextBox1.SelectionStart = richTextBox1.Text.Length;
			richTextBox1.SelectionColor = Color.Black;

			_tagBox.ScrollAlwaysVisible = false;
			_tagBox.IntegralHeight = true;
			_tagBox.ItemHeight = richTextBox1.Font.Height;
			_tagBox.Size = new Size(400, richTextBox1.Font.Height);
			_tagBox.KeyDown += Tagbox_KeyDown;
			_tagBox.MouseDoubleClick += TagBoxOnMouseDoubleClick;
		}

		private void TagBoxOnMouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
		{
			if (_tagBox.SelectedItems.Count == 1)
				ApplyTagNameToRichTextBox();
		}

		#region TextFunctions

		private void Undo()
		{
			richTextBox1.Undo();
		}

		private void Redo()
		{
			richTextBox1.Redo();
		}

		private void Cut()
		{
			richTextBox1.Cut();
		}

		private void Copy()
		{
			richTextBox1.Copy();
		}

		private void Paste()
		{
			richTextBox1.Paste();
		}

		private void SelectAll()
		{
			richTextBox1.SelectAll();
		}

		#endregion

		#region GuiFunctions

		//private void HandleAppShortCuts(KeyEventArgs e)
		//{
		//	if (e.Modifiers == (Keys)Enum.Parse(typeof(Keys), "Alt", true)
		//				 && e.KeyCode == (Keys)Enum.Parse(typeof(Keys), "l", true))
		//	{
		//		PanelLShowHide();
		//		_menuLock = true;
		//	}
		//	else if (e.Modifiers == (Keys)Enum.Parse(typeof(Keys), "Alt", true)
		//		&& e.KeyCode == (Keys)Enum.Parse(typeof(Keys), "r", true))
		//	{
		//		PanelRShowHide();
		//		_menuLock = true;
		//	}
		//	else if (e.KeyCode == Keys.Menu && _menuLock == false)
		//	{
		//		MenuStripShowHide();
		//	}
		//	else if (e.KeyCode == Keys.Menu)
		//	{
		//		_menuLock = false;
		//	}

		//	splitContainer2.Focus();
		//}

		private void PrintStartingTime(string devider)
		{
			string date;
			date = DateTime.Now.ToShortTimeString();
			richTextBox1.AppendText(date + devider);
			//ParseWord();
			//richTextBox1.SelectionStart = richTextBox1.Text.Length - (date + devider).Length;
			//richTextBox1.SelectionLength = 8;
			//richTextBox1.SelectionColor = Color.RoyalBlue;
			//richTextBox1.SelectionStart = richTextBox1.Text.Length;
			//richTextBox1.SelectionColor = Color.Black;
		}

		#endregion

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
				richTextBox1.Focus();
			}
			else
			{
				richTextBox2.Focus();
			}
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
			if (e.KeyCode == Keys.Back && !string.IsNullOrWhiteSpace(richTextBox1.Text))
			{
				if (IsTagIdentifier(richTextBox1.Text[richTextBox1.SelectionStart - 1].ToString()))
				{
					CancelTagDetection();
				}
			}

			//navigate through tagBox
			if (_tagBox.Items.Count > 0
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

		private void CancelTagDetection()
		{
			_tagBox.Items.Clear();
			richTextBox1.Controls.Clear();
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			CancelTagDetection();
			string word = GetWordAtTextCursor(false);
			ParseWord(word);
			richTextBox1.SelectionColor = Color.Black;
		}

		private void richTextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			//if ((ModifierKeys & Keys.Control) == Keys.Control
			//	&& e.KeyChar == ' ')
			//{
			//	string word = GetWordAtTextCursor(false);
			//	if (!string.IsNullOrWhiteSpace(word)
			//		&& IsTagIdentifier(word.Remove(1, word.Length - 1))
			//		&& DetectTag())
			//	{
			//		FillAndPlaceListBox(_columnNumber, _tagName);
			//	}
			//	e.Handled = true;
			//}
			////else if (IsTagIdentifier(e.KeyChar.ToString()))
			////{
			////	ApplyTagNameToRichTextBox();
			////}
			//else if ((ModifierKeys & Keys.Control) == Keys.Control
			//	&& e.KeyChar.ToString() == "\n")
			//{
			//	PrintStartingTime("\t");
			//}
			////else if (IsTagIdentifier(e.KeyChar.ToString()))
			////{
			////	tagDetectionActive = true;
			////	TagIdentifier.TryGetValue(e.KeyChar.ToString(), out currentTagType);
			////	tagName = string.Empty;
			////}
		}

		private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
		{
			//GetLineAndColumnNumber(out _line, out _columnNumber);
			//string key = e.KeyData.ToString();
			//if (key.Length == 1)
			//{
			//	char token = key.ToCharArray()[0];
			//	if (char.IsLetter(token) && DetectTag())
			//	{
			//		FillAndPlaceListBox(_columnNumber, _tagName);
			//	}
			//}
			//else if (e.KeyData == Keys.Back
			//	&& _tagDetectionActive
			//	&& DetectTag())
			//{
			//	FillAndPlaceListBox(_columnNumber, _tagName);
			//}
		}

		private void ParseWord(string word = "")
		{
			string datePattern = @"(?<" + PatternGroups.date.ToString() + @">[0-9]{2}.[0-9]{2}.[0-9]{4})";
			string timePattern = @"(?<" + PatternGroups.time.ToString() + @">[0-9]{2}:[0-9]{2})";
			string customerPattern = @"(?<" + PatternGroups.customer.ToString() + @"><[A-Za-z]{1,})";
			string projectPattern = @"(?<" + PatternGroups.project.ToString() + @">>[A-Za-z]{1,})";
			string servicePattern = @"(?<" + PatternGroups.service.ToString() + @">'[A-Za-z]{1,})";
			string taskPattern = @"(?<" + PatternGroups.task.ToString() + @">#[0-9]{1,})";
			string wordPattern = @"(?<" + PatternGroups.text.ToString() + @">\w+)";

			string pattern = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
				datePattern,
				timePattern,
				customerPattern,
				projectPattern,
				servicePattern,
				taskPattern,
				wordPattern);

			Regex myRegex = new Regex(pattern, RegexOptions.None);

			int startPosition = richTextBox1.SelectionStart;
			int absoluteIndex = richTextBox1.GetFirstCharIndexOfCurrentLine();

			string evalText;
			if (word != "")
			{
				evalText = @word;
			}
			else
			{
				evalText = string.Empty;
			}

			foreach (Match myMatch in myRegex.Matches(evalText))
			{
				if (myMatch.Groups[PatternGroups.customer.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.customer.ToString(), Color.ForestGreen, TagType.Customer);
				}
				else if (myMatch.Groups[PatternGroups.project.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.project.ToString(), Color.DarkOrange, TagType.Project);
				}
				else if (myMatch.Groups[PatternGroups.service.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.service.ToString(), Color.DeepSkyBlue, TagType.Service);
				}
				else if (myMatch.Groups[PatternGroups.task.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.task.ToString(), Color.Purple, TagType.Task);
				}
				else if (myMatch.Groups[PatternGroups.time.ToString()].Success)
				{
					SetColor(absoluteIndex, myMatch, Color.RoyalBlue, word, startPosition, startPosition);
				}
				else if (myMatch.Groups[PatternGroups.date.ToString()].Success)
				{
					SetColor(absoluteIndex, myMatch, Color.Crimson, word, startPosition, startPosition);
				}
				else
				{
					CancelTagDetection();
				}
			}
			richTextBox1.SelectionStart = startPosition;
			richTextBox1.SelectionLength = 0;
		}

		private void SetColorAndFillTagBox(string word, Match myMatch, int absoluteIndex,
			int startPosition, string regexGroup, Color color, TagType tagType)
		{
			var matchString = myMatch.Groups[regexGroup].Value;
			SetColor(absoluteIndex, myMatch, color, word, startPosition - matchString.Length, startPosition);
			var tagName = myMatch.Groups[regexGroup].Value.Remove(0, 1);
			FillAndPlaceListBox(startPosition, tagName, tagType);
		}

		private void SetColor(int absoluteIndex, Match myMatch, Color color,
			string word = "", int from = 0, int to = 0)
		{
			if (word != "")
			{
				var found = 0;
				string searchWord = word;
				int substract = 0;
				if (IsTagIdentifier(word.Substring(0, 1)))
				{
					searchWord = word.Remove(0, 1);
					substract = 1;
				}
				found = richTextBox1.Find(searchWord, from, RichTextBoxFinds.WholeWord) - substract;
				richTextBox1.SelectionStart = found >= 0 ? found : 0;
				richTextBox1.SelectionLength = myMatch.Length;
				richTextBox1.SelectionColor = color;
			}
			else if (!string.IsNullOrEmpty(richTextBox1.Text))
			{
				richTextBox1.SelectionStart = absoluteIndex + myMatch.Index;
			}
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
			richTextBox1.Focus();
			base.OnShown(e);
		}

		#endregion

		#region Logic

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

		private bool DetectTag()
		{
			string wordAtCursor = GetWordAtTextCursor(false);
			if (!string.IsNullOrWhiteSpace(wordAtCursor))
			{
				char tagCandidate = wordAtCursor.ToCharArray()[0];
				if (IsTagIdentifier(tagCandidate.ToString()))
				{
					_tagDetectionActive = true;
					_tagIdentifier.TryGetValue(tagCandidate.ToString(), out _currentTagType);
					_tagName = wordAtCursor.Remove(0, 1);
					return true;
				}
				else
				{
					_tagDetectionActive = false;
					return false;
				}
			}
			return false;
		}

		private void ApplyTagNameToRichTextBox()
		{
			if (_tagBox.SelectedItem != null)
			{
				int n = _columnNumber - 1;
				string tagSign = string.Empty;

				int selectionStart = richTextBox1.SelectionStart;
				int selectionLength = 0;

				for (int i = selectionStart - 1; i > 0; i--)
				{
					if (IsTagIdentifier(richTextBox1.Text[i].ToString()))
					{
						if (richTextBox1.Text[i].ToString() == _customerTagSign)
						{
							//tagSign = richTextBox1.Text[i].ToString();
							_activeCustomer = _tagBox.SelectedItem.ToString();
						}
						break;
					}
					selectionStart--;
					selectionLength++;
				}

				richTextBox1.SelectionStart = selectionStart;
				richTextBox1.SelectionLength = selectionLength;
				richTextBox1.SelectedText = _tagBox.SelectedItem.ToString();

				//if (!string.IsNullOrWhiteSpace(tagSign))
				//{
				//	_activeCustomer = _tagBox.SelectedItem.ToString();
				//}

				_tagBox.Items.Clear();
				_tagDetectionActive = false;
				richTextBox1.Controls.Clear();
			}
		}

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			richTextBox1.Text = monthCalendar1.SelectionEnd.ToShortDateString();
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

		private string GetCharsBeforeCursor()
		{
			if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
			{
				int i = richTextBox1.SelectionStart - 1;
				string charsBeforeCursor = string.Empty;
				while (i >= 0 && !string.IsNullOrWhiteSpace(richTextBox1.Text[i].ToString()))
				{
					charsBeforeCursor += richTextBox1.Text[i];
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
			int i = richTextBox1.SelectionStart;
			string charsAfterCursor = string.Empty;
			while (richTextBox1.TextLength > i
				&& !string.IsNullOrWhiteSpace(richTextBox1.Text[i].ToString())
				&& !IsTagIdentifier(richTextBox1.Text[i].ToString()))
			{
				charsAfterCursor += richTextBox1.Text[i];
				i++;
			}
			return charsAfterCursor;
		}

		private bool IsTagIdentifier(char[] searchLine, int i)
		{
			return Regex.Match(searchLine[i].ToString(), "[$&+#]").Success;
		}

		private bool IsTagIdentifier(string e)
		{
			return Regex.Match(e, _tagPattern).Success;
		}

		private string ReverseCharsBeforeCursor(string charsBeforeCursor)
		{
			char[] charArray = charsBeforeCursor.ToCharArray();
			Array.Reverse(charArray);
			charsBeforeCursor = new string(charArray);
			return charsBeforeCursor;
		}

		private void FillAndPlaceListBox(int tokenPosition, string tagName, TagType CurrentTagType)
		{
			//int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);

			_tagBox.Items.Clear();
			_tagBox.Items.AddRange(items: GetAllSuitableTags(tagName, CurrentTagType).ToArray());
			if (_tagBox.Items.Count == 0)
			{
				richTextBox1.Controls.Clear();
				_tagBox.Items.Clear();
				return;
			}
			int height = (richTextBox1.Font.Height + 10) * _tagBox.Items.Count;
			int width = 423;
			Size s = new Size();
			s.Height = height;
			s.Width = width;
			_tagBox.Size = s;
			richTextBox1.Controls.Add(_tagBox);
			Point point = richTextBox1.GetPositionFromCharIndex(tokenPosition);
			//point.Y += richTextBox1.Font.Height * (lineNumber + 1);
			point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + 2;
			point.X += 2;
			_tagBox.Location = point;
			_tagBox.SelectedIndex = 0;
		}

		private List<string> GetAllSuitableTags(string tagName, TagType currenTagType)
		{
			List<string> result = new List<string>();
			switch (currenTagType)
			{
				case TagType.Customer:
					result = _mcm.Customers.GetCustomerNames(tagName);
					break;
				case TagType.Project:
					result = _mcm.Projects.GetMatchedProjectNames(tagName, _activeCustomer);
					break;
				case TagType.Service:
					result = _mcm.Services.GetMachedServiceNames(tagName);
					break;
				case TagType.Task:
					//TODO trello-API implementieren
					;
					break;
			}
			return result;
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
	}
}
