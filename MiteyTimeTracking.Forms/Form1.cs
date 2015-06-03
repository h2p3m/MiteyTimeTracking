using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
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

	public partial class Form1 : Form
	{
		private bool menuLock = false;
		private string richTextBoxName = string.Empty;
		private string tagName = string.Empty;
		private bool tagDetectionActive;
		private MiteConnectorModel mcm;
		private Dictionary<String, TagType> TagIdentifier = new Dictionary<string, TagType>();
		private TagType currentTagType = TagType.Customer;
		private ListBox tagBox = new ListBox();
		private string customerTagSign = "<";
		private string projectTagSign = ">";
		private string serviceTagSign = "'";
		private string taskTagSign = "#";
		private string tagPattern = string.Empty;
		private string line = string.Empty;
		private int columnNumber = 0;
		private string activeCustomer = string.Empty;

		public Form1()
		{
			InitializeComponent();

			try
			{
				mcm = new MiteConnectorModel();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Es gab ein Problem mit der Mite-API:\n\n" + ex.Message, "Fehler",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			tagPattern = string.Format("[{0}{1}{2}{3}]"
				, customerTagSign
				, projectTagSign
				, serviceTagSign
				, taskTagSign);

			TagIdentifier.Add(customerTagSign, TagType.Customer);
			TagIdentifier.Add(projectTagSign, TagType.Project);
			TagIdentifier.Add(serviceTagSign, TagType.Service);
			TagIdentifier.Add(taskTagSign, TagType.Task);

			this.KeyPreview = true;

			splitContainer1.Panel1Collapsed = true;
			splitContainer2.Panel2Collapsed = true;
			splitContainer1.SplitterDistance = 340;

			richTextBox1.Text = DateTime.Now.ToShortDateString() + " ";
			richTextBox1.Find(DateTime.Now.ToShortDateString());
			richTextBox1.SelectionColor = Color.Sienna;
			richTextBoxName = richTextBox1.Name;
			richTextBox1.SelectionStart = richTextBox1.Text.Length;
			richTextBox1.SelectionColor = Color.Black;

			tagBox.ScrollAlwaysVisible = false;
			tagBox.IntegralHeight = true;
			tagBox.ItemHeight = richTextBox1.Font.Height;
			tagBox.Size = new Size(400, richTextBox1.Font.Height);
			tagBox.KeyDown += new KeyEventHandler(this.Tagbox_KeyDown);
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

		private void HandleAppShortCuts(KeyEventArgs e)
		{
			if (e.Modifiers == (Keys)Enum.Parse(typeof(Keys), "Alt", true)
						 && e.KeyCode == (Keys)Enum.Parse(typeof(Keys), "l", true))
			{
				PanelLShowHide();
				menuLock = true;
			}
			else if (e.Modifiers == (Keys)Enum.Parse(typeof(Keys), "Alt", true)
				&& e.KeyCode == (Keys)Enum.Parse(typeof(Keys), "r", true))
			{
				PanelRShowHide();
				menuLock = true;
			}
			else if (e.KeyCode == Keys.Menu && menuLock == false)
			{
				MenuStripShowHide();
			}
			else if (e.KeyCode == Keys.Menu)
			{
				menuLock = false;
			}

			splitContainer2.Focus();
		}

		private void PrintStartingTime(string devider)
		{
			string date;
			date = DateTime.Now.ToShortTimeString();
			richTextBox1.AppendText(date + devider);
			ParseLine();
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
			if (e.KeyCode == Keys.Menu && menuLock == false)
			{
				MenuStripShowHide();
				menuLock = false;
				e.Handled = true;
			}
			if (e.KeyCode == Keys.Menu && menuLock == true)
			{
				menuLock = false;
				e.Handled = true;
			}
		}

		private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
		{
			//tag indicator deleted
			if (e.KeyCode == Keys.Back && !string.IsNullOrWhiteSpace(line))
			{
				GetLineAndColumnNumber(out line, out columnNumber);
				int tokenPosition = columnNumber > 0 ? columnNumber - 1 : 0;
				char token = line[tokenPosition];
				string tokenString = token.ToString();

				if (IsTagIdentifier(tokenString))
				{
					CancelTagDetection();
				}
			}
			
			//tag indicator selected
			else if (tagBox.SelectedItem != null
				&& tagDetectionActive == true
				&& (e.KeyData == Keys.Enter
				|| e.KeyData == Keys.Tab
				|| e.KeyData == Keys.Space
				|| e.KeyData == Keys.OemPeriod))
			{
				ApplyTagNameToRichTextBox();
				e.Handled = true;
			}
			
			//navigate through tagBox
			else if (tagBox.Items.Count > 0
				&& (e.KeyData == Keys.Up || e.KeyData == Keys.Down))
			{
				if (e.KeyData == Keys.Up && tagBox.SelectedIndex > 0)
				{
					tagBox.SelectedIndex--;
				}
				else if (e.KeyData == Keys.Down && tagBox.SelectedIndex < tagBox.Items.Count - 1)
				{
					tagBox.SelectedIndex++;
				}

				if (e.KeyData == Keys.Up || e.KeyData == Keys.Down)
				{
					e.Handled = true;
				}
			}

			//cancel tagDetection
			else if (e.KeyData == Keys.Escape)
			{
				CancelTagDetection();
			}
		}

		private void CancelTagDetection()
		{
			tagDetectionActive = false;
			activeCustomer = string.Empty;
			tagBox.Items.Clear();
			richTextBox1.Controls.Clear();
		}

		private void richTextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control
				&& e.KeyChar == ' ')
			{
				string word = GetWordAtCursor(false);
				if (!string.IsNullOrWhiteSpace(word)
					&& IsTagIdentifier(word.Remove(1, word.Length - 1))
					&& DetectTag())
				{
					FillAndPlaceListBox(columnNumber, tagName);
				}
				e.Handled = true;
			}
			else if (IsTagIdentifier(e.KeyChar.ToString()))
			{
				ApplyTagNameToRichTextBox();
			}
			else if ((Control.ModifierKeys & Keys.Control) == Keys.Control
				&& e.KeyChar.ToString() == "\n")
			{
				PrintStartingTime(":> ");
			}
			//else if (IsTagIdentifier(e.KeyChar.ToString()))
			//{
			//	tagDetectionActive = true;
			//	TagIdentifier.TryGetValue(e.KeyChar.ToString(), out currentTagType);
			//	tagName = string.Empty;
			//}
		}

		private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
		{
			ParseLine();

			GetLineAndColumnNumber(out line, out columnNumber);
			string key = e.KeyData.ToString();
			if (key.Length == 1)
			{
				char token = key.ToCharArray()[0];
				if (char.IsLetter(token) && DetectTag())
				{
					FillAndPlaceListBox(columnNumber, tagName);
				}
			}
			else if (e.KeyData == Keys.Back 
				&& tagDetectionActive == true
				&& DetectTag())
			{
				FillAndPlaceListBox(columnNumber, tagName);
			}
		}

		private void ParseLine()
		{
			string datePattern = @"(?<date>[0-9]{2}.[0-9]{2}.[0-9]{4})";
			string timePattern = @"(?<time>[0-9]{2}:[0-9]{2}:>)";
			string customerPattern = @"(?<customer><[A-Za-z]\w+)";
			string projectPattern = @"(?<project>>[A-Za-z]\w+)";
			string servicePattern = @"(?<service>'[A-Za-z]\w+)";
			string taskPattern = @"(?<task>#[0-9]{1,})";

			string pattern = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
				datePattern,
				timePattern,
				customerPattern,
				projectPattern,
				servicePattern,
				taskPattern);

			Regex myRegex = new Regex(pattern, RegexOptions.None);

			int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);

			foreach (Match myMatch in myRegex.Matches(richTextBox1.Lines[lineNumber]))
			{
				if (myMatch.Groups["time"].Success)
				
				{
					int absoluteIndex = richTextBox1.GetFirstCharIndexOfCurrentLine();
					richTextBox1.SelectionStart = absoluteIndex + myMatch.Index;
					richTextBox1.SelectionLength = absoluteIndex + myMatch.Length;
					richTextBox1.SelectionColor = Color.RoyalBlue;
					richTextBox1.SelectionStart = absoluteIndex + myMatch.Index + myMatch.Length;
					richTextBox1.SelectionColor = Color.Black;
				}
			}
		}

		private void Tagbox_KeyDown(object sender, KeyEventArgs e)
		{
			ApplyTagNameToRichTextBox();
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

		private string GetWordAtCursor(bool cutTagIndicator)
		{
			string charsAfterCursor = GetCharsAfterCursor(line, columnNumber);
			string charsBeforeCursor = GetCharsBeforeCursor(line, columnNumber);

			string wordAtCursor = charsBeforeCursor + charsAfterCursor;

			if (string.IsNullOrWhiteSpace(wordAtCursor))
				return string.Empty;
			if (cutTagIndicator)
				return (wordAtCursor).Remove(0, 1);
			return wordAtCursor;
		}

		private bool DetectTag()
		{
			string wordAtCursor = GetWordAtCursor(false);
			if (!string.IsNullOrWhiteSpace(wordAtCursor))
			{
				char tagCandidate = wordAtCursor.ToCharArray()[0];
				if (IsTagIdentifier(tagCandidate.ToString()))
				{
					tagDetectionActive = true;
					TagIdentifier.TryGetValue(tagCandidate.ToString(), out currentTagType);
					tagName = wordAtCursor.Remove(0, 1);
					return true;
				}
				else
				{
					tagDetectionActive = false;
					return false;
				}
			}
			return false;
		}

		private void ApplyTagNameToRichTextBox()
		{
			if (tagBox.SelectedItem != null)
			{
				GetLineAndColumnNumber(out line, out columnNumber);
				int n = columnNumber - 1;
				char[] charsInLine = line.ToCharArray();
				string tagSign = string.Empty;

				int selectionStart = richTextBox1.SelectionStart;
				int selectionLength = 0;

				for (int i = n; i > 0; i--)
				{
					if (IsTagIdentifier(charsInLine[i].ToString()))
					{
						if (charsInLine[i].ToString() == customerTagSign)
						{
							tagSign = charsInLine[i].ToString();
						}
						break;
					}
					selectionStart--;
					selectionLength++;
				}

				richTextBox1.SelectionStart = selectionStart;
				richTextBox1.SelectionLength = selectionLength;
				richTextBox1.SelectedText = tagBox.SelectedItem.ToString();

				if (!string.IsNullOrWhiteSpace(tagSign))
				{
					activeCustomer = tagBox.SelectedItem.ToString();
				}

				tagBox.Items.Clear();
				tagDetectionActive = false;
				richTextBox1.Controls.Clear();
			}
		}

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			richTextBox1.Text = monthCalendar1.SelectionEnd.ToShortDateString();
		}

		private void showOrHideMenuToolStripMenuItem_Click(object sender, EventArgs e)
		{
			menuLock = true;
			MenuStripShowHide();
		}

		private void showOrHideLeftPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			menuLock = true;
			PanelLShowHide();
		}

		private void showOrHideRightPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			menuLock = true;
			PanelRShowHide();
		}

		private string GetCharsBeforeCursor(string line, int columnNumber)
		{
			char[] searchLine = line.ToCharArray();
			int i = columnNumber - 1;
			string charsBeforeCursor = string.Empty;
			while (i >= 0 && !string.IsNullOrWhiteSpace(searchLine[i].ToString()))
			{
				charsBeforeCursor += line[i];
				if (IsTagIdentifier(charsBeforeCursor))
				{
					return ReverseCharsBeforeCursor(charsBeforeCursor);
				}
				i--;
			}
			return ReverseCharsBeforeCursor(charsBeforeCursor);
		}

		private string GetCharsAfterCursor(string line, int columnNumber)
		{
			char[] searchLine = line.ToCharArray();
			int i = columnNumber;
			string charsAfterCursor = string.Empty;
			while (line.Length > i && !string.IsNullOrWhiteSpace(searchLine[i].ToString())
				&& !IsTagIdentifier(searchLine[i].ToString()))
			{
				charsAfterCursor += line[i];
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
			return Regex.Match(e, tagPattern).Success;
		}

		private string ReverseCharsBeforeCursor(string charsBeforeCursor)
		{
			char[] charArray = charsBeforeCursor.ToCharArray();
			Array.Reverse(charArray);
			charsBeforeCursor = new string(charArray);
			return charsBeforeCursor;
		}

		private void GetLineAndColumnNumber(out string line, out int columnNumber)
		{
			int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
			line = richTextBox1.Lines[lineNumber];
			columnNumber = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexFromLine(lineNumber);
		}

		private void FillAndPlaceListBox(int tokenPosition, string tagName)
		{
			int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);

			tagBox.Items.Clear();
			tagBox.Items.AddRange(GetAllSuitableTags(tagName).ToArray());
			if (tagBox.Items.Count == 0)
			{
				richTextBox1.Controls.Clear();
				tagBox.Items.Clear();
				return;
			}
			int height = (richTextBox1.Font.Height + 10) * tagBox.Items.Count;
			int width = 423;
			Size s = new Size();
			s.Height = height;
			s.Width = width;
			tagBox.Size = s;
			richTextBox1.Controls.Add(tagBox);
			Point p = richTextBox1.GetPositionFromCharIndex(tokenPosition);
			p.Y += richTextBox1.Font.Height * (lineNumber + 1);
			tagBox.Location = p;
			tagBox.SelectedIndex = 0;
		}

		private List<string> GetAllSuitableTags(string tagName)
		{
			List<string> result = new List<string>();
			switch (currentTagType)
			{
				case TagType.Customer:
					result = mcm.Customers.GetCustomerNames(tagName);
					break;
				case TagType.Project:
					result = mcm.Projects.GetMatchedProjectNames(tagName, activeCustomer);
					break;
				case TagType.Service:
					result = mcm.Services.GetMachedServiceNames(tagName);
					break;
				case TagType.Task:
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
