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

namespace MiteyTimeTracking.Forms
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

			TagIdentifier.Add("$", TagType.Customer);
			TagIdentifier.Add("&", TagType.Project);
			TagIdentifier.Add("+", TagType.Service);
			TagIdentifier.Add("#", TagType.Task);

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
			richTextBox1.SelectionStart = richTextBox1.Text.Length - (date + devider).Length;
			richTextBox1.SelectionLength = 8;
			richTextBox1.SelectionColor = Color.RoyalBlue;
			richTextBox1.SelectionStart = richTextBox1.Text.Length;
			richTextBox1.SelectionColor = Color.Black;
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

		//protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		//{
		//	if (keyData == (Keys.Alt | Keys.L))
		//	{
		//		PanelLShowHide();
		//		return true;
		//	}
		//	else if (keyData == (Keys.Alt | Keys.R))
		//	{
		//		PanelRShowHide();
		//		return true;
		//	}
		//	else if (keyData == Keys.Alt && menuLock == false)
		//	{
		//		MenuStripShowHide();
		//		return true;
		//	}

		//	return base.ProcessCmdKey(ref msg, keyData);
		//}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Menu && menuLock == false)
			{
				MenuStripShowHide();
				menuLock = false;
			}
			if (e.KeyCode == Keys.Menu && menuLock == true)
			{
				menuLock = false;
			}
			e.Handled = true;
		}

		private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
		{
			string line;
			int columnNumber;
			GetLineAndColumnNumber(out line, out columnNumber);

			string tagPattern = "[$&+#]";

			if (e.KeyCode == Keys.Back && Regex.Match(line[columnNumber - 1].ToString(), tagPattern).Success)
			{
				tagDetectionActive = false;
				tagName = string.Empty;
				PopulateListBox(columnNumber - 1);
			}
			//TODO auch Space und andere nicht-Buchstaben-Tasten einschließen
			else if (e.KeyCode == Keys.Back
				&& tagName.Length > 0)
			{
				tagName = tagName.Remove(tagName.Length - 1, 1);
				PopulateListBox(columnNumber - 1);
			}
		}

		private void richTextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			string line;
			int columnNumber;
			GetLineAndColumnNumber(out line, out columnNumber);

			string tagPattern = "[$&+#]";

			if (tagDetectionActive
				&& char.IsLetter(e.KeyChar))
			{
				tagName += e.KeyChar;
				if (tagName.Count() >= 1)
				{
					PopulateListBox(columnNumber);
				}
			}

			if ((Control.ModifierKeys & Keys.Control) == Keys.Control
				&& e.KeyChar.ToString() == "\n")
			{
				string devider = ":> ";
				PrintStartingTime(devider);
			}
			//else if (tagDetectionActive && e.KeyChar.ToString() == Keys.Space.ToString())
			//{
			//	tagDetectionActive = false;
			//	tagName = string.Empty;
			//}
			else if (Regex.Match(e.KeyChar.ToString(), tagPattern).Success)
			{
				tagDetectionActive = true;
				TagIdentifier.TryGetValue(e.KeyChar.ToString(), out currentTagType);
			}
			else
			{
				char[] searchLine = line.ToCharArray();
				int i = columnNumber;
				string charactersAfterCursor = string.Empty;
				while (line.Length > i && !string.IsNullOrWhiteSpace(searchLine[i].ToString()))
				{
					charactersAfterCursor += line[i];
					i++;
				}
				i = columnNumber - 1;
				string charactersBeforeCursor = string.Empty;
				while (i >= 0 && !string.IsNullOrWhiteSpace(searchLine[i].ToString()))
				{
					charactersBeforeCursor += line[i];
					i--;
				}
				char[] charArray = charactersBeforeCursor.ToCharArray();
				Array.Reverse(charArray);
				charactersBeforeCursor = new string(charArray);
				string searchWord = charactersBeforeCursor + charactersAfterCursor;
			}
		}

		private void GetLineAndColumnNumber(out string line, out int columnNumber)
		{
			int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
			line = richTextBox1.Lines[lineNumber];
			columnNumber = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexFromLine(lineNumber);
		}

		private void PopulateListBox(int columnNumber)
		{
			if (tagName.Length >= 1)
			{
				tagBox.Items.Clear();
				tagBox.Items.AddRange(FindTag(tagName).ToArray());
				if (tagBox.Items.Count == 0)
				{
					richTextBox1.Controls.Clear();
					return;
				}
				int height = (richTextBox1.Font.Height + 10) * tagBox.Items.Count;
				int width = 423;
				Size s = new Size();
				s.Height = height;
				s.Width = width;
				tagBox.Size = s;
				richTextBox1.Controls.Add(tagBox);
				Point p = richTextBox1.GetPositionFromCharIndex(columnNumber);
				p.Y += richTextBox1.Font.Height;
				tagBox.Location = p;
			}
			else
			{
				richTextBox1.Controls.Clear();
			}
		}

		private List<string> FindTag(string tagName)
		{
			List<string> result = new List<string>();
			switch (currentTagType)
			{
				case TagType.Customer:
					result = mcm.Customers.GetCustomerNames(tagName);
					break;
				case TagType.Project:
					result = mcm.Projects.GetMatchedProjectNames(tagName);
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

		private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			richTextBox1.Text = monthCalendar1.SelectionEnd.ToShortDateString();
		}

		private void richTextBox1_Leave(object sender, EventArgs e)
		{
			tagDetectionActive = false;
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
