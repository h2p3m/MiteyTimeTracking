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
		private string richTextBoxName;
		private string tagName;
		private bool tagDetectionActive;
		private MiteConnectorModel mcm;
		private Dictionary<String, TagType> TagIdentifier = new Dictionary<string, TagType>();
		private TagType currentTagType = TagType.Customer;

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
			if (e.Modifiers == (Keys)Enum.Parse(typeof(Keys), "Alt", true)
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

			this.richTextBox1.Focus();

			//SetTextBoxFocus();

			//PrintFocus();
		}

		private void SetTextBoxFocus()
		{
			if (menuStrip1.Visible == false || splitContainer1.Panel1Collapsed)
			{
				if (splitContainer2.Panel2Collapsed)
				{
					richTextBox1.Focus();
					this.ActiveControl = richTextBox1;
				}
				else if (splitContainer2.Panel2Collapsed == false)
				{
					richTextBox2.Focus();
					this.ActiveControl = richTextBox2;
				}
			}

		}

		private void PanelLShowHide()
		{
			splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;

			this.richTextBox1.Focus();

			//SetTextBoxFocus();

			//PrintFocus();
		}

		private void PanelRShowHide()
		{
			splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;

			if (splitContainer2.Panel2Collapsed)
			{
				this.richTextBox1.Focus();
			}
			else
			{
				this.richTextBox2.Select();
			}
		}

		private void PrintFocus()
		{
			Console.WriteLine("\n----------------------");
			Console.WriteLine("1: " + richTextBox1.Focused);
			Console.WriteLine("2: " + richTextBox2.Focused);
			Console.WriteLine(this.ActiveControl.ToString());
			Console.WriteLine("----------------------\n");
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
			HandleAppShortCuts(e);
			//SetTextBoxFocus();
			this.richTextBox1.Focus();
		}

		private void richTextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
			string line = richTextBox1.Lines[lineNumber];
			int columnNumber = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexFromLine(lineNumber);
			var selStart = richTextBox1.SelectionStart;

			string tagPattern = "[$&+#]";

			if (e.KeyChar == '\b' && Regex.Match(line[columnNumber - 1].ToString(), tagPattern).Success)
			{
				tagDetectionActive = false;
			}

			if (tagDetectionActive)
			{
				tagName += e.KeyChar;
				if (tagName.Count() >= 1)
				{
					FindTag(tagName);
				}
			}

			if ((Control.ModifierKeys & Keys.Control) == Keys.Control
				&& e.KeyChar.ToString() == "\n")
			{
				string devider = ":> ";
				PrintStartingTime(devider);
			}
			else if (tagDetectionActive && e.KeyChar.ToString() == Keys.Space.ToString())
			{
				tagDetectionActive = false;
				tagName = string.Empty;
			}
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

		private void FindTag(string tagName)
		{
			//ab hier Mite-API einbinden
			//Liste aller Tags vom gesuchten Typ geben
			//Like/Suche über Liste mit tagName

			//Zugriff auf das MiteModel im DAL
			switch (currentTagType)
			{
				case TagType.Customer:
					var c = mcm.Customers.GetCustomerNames(tagName);
					break;
				case TagType.Project:
					var p = mcm.Projects.GetMatchedProjectNames(tagName);
					break;
				case TagType.Service:
					var s = mcm.Services.GetMachedServiceNames(tagName);
					break;
				case TagType.Task:
					;
					break;
			}
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			Control c = sender as Control;
			RichTextBox rtb = c.Controls.Find(richTextBoxName, true).FirstOrDefault() as RichTextBox;
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
