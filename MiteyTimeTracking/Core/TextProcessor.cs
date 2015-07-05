using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MiteyTimeTracking.Enums;

namespace MiteyTimeTracking.Core
{
	public class TextProcessor
	{
		private Form1 _form1;

		private static string datePattern = @"(?<" + PatternGroups.Date.ToString() + @">[0-9]{2}.[0-9]{2}.[0-9]{4})";
		private static string timePattern = @"(?<" + PatternGroups.Time.ToString() + @">[0-9]{2}:[0-9]{2})";
		private static string customerPattern = @"(?<" + PatternGroups.Customer.ToString() + @"><[A-Za-z]{1,})";
		private static string projectPattern = @"(?<" + PatternGroups.Project.ToString() + @">>[A-Za-z]{1,})";
		private static string servicePattern = @"(?<" + PatternGroups.Service.ToString() + @">'[A-Za-z]{1,})";
		private static string taskPattern = @"(?<" + PatternGroups.Task.ToString() + @">#[0-9]{3,})";
		//string wordPattern = @"(?<" + PatternGroups.text.ToString() + @">\w+)";

		private string pattern = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
			customerPattern,
			projectPattern,
			servicePattern,
			taskPattern,
			timePattern,
			datePattern);

		public TextProcessor(Form1 form1)
		{
			_form1 = form1;
		}

		public void ParseWord(string word = "")
		{
			Regex myRegex = new Regex(pattern, RegexOptions.None);

			int startPosition = _form1.mainTextBox.SelectionStart;
			int absoluteIndex = _form1.mainTextBox.GetFirstCharIndexOfCurrentLine();

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
				if (myMatch.Groups[PatternGroups.Customer.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.Customer.ToString(), Color.ForestGreen, TagType.Customer);
				}
				else if (myMatch.Groups[PatternGroups.Project.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.Project.ToString(), Color.DarkOrange, TagType.Project);
					_form1.ActiveCustomer = string.Empty;
				}
				else if (myMatch.Groups[PatternGroups.Service.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.Service.ToString(), Color.DeepSkyBlue, TagType.Service);
				}
				else if (myMatch.Groups[PatternGroups.Task.ToString()].Success)
				{
					SetColorAndFillTagBox(word, myMatch, absoluteIndex, startPosition,
						PatternGroups.Task.ToString(), Color.Purple, TagType.Task);
				}
				else if (myMatch.Groups[PatternGroups.Time.ToString()].Success)
				{
					SetColor(absoluteIndex, myMatch, Color.RoyalBlue, word, startPosition - myMatch.Length);
				}
				else if (myMatch.Groups[PatternGroups.Date.ToString()].Success)
				{
					SetColor(absoluteIndex, myMatch, Color.Crimson, word, startPosition - myMatch.Length);
				}
				else
				{
					_form1.CancelTagDetection();
				}
			}

			_form1.mainTextBox.SelectionStart = startPosition;
			_form1.mainTextBox.SelectionLength = 0;
		}

		private void SetColorAndFillTagBox(string word, Match myMatch, int absoluteIndex,
			int startPosition, string regexGroup, Color color, TagType tagType)
		{
			var matchString = myMatch.Groups[regexGroup].Value;
			SetColor(absoluteIndex, myMatch, color, word, startPosition - matchString.Length);
			var tagName = myMatch.Groups[regexGroup].Value.Remove(0, 1);
			_form1.FillAndPlaceListBox(startPosition, tagName, tagType);
		}

		private void SetColor(int absoluteIndex, Match myMatch, Color color,
			string word, int from)
		{
			if (!string.IsNullOrWhiteSpace(word))
			{
				string searchWord = word;
				int substract = 0;
				if (_form1.IsTagIdentifier(word.Substring(0, 1)))
				{
					searchWord = word.Remove(0, 1);
					substract = 1;
				}
				var found = _form1.mainTextBox.Find(searchWord, @from >= 0 ? @from : 0,
					RichTextBoxFinds.WholeWord) - substract;
				_form1.mainTextBox.SelectionStart = found >= 0 ? found : 0;
				_form1.mainTextBox.SelectionLength = myMatch.Length;
				_form1.mainTextBox.SelectionColor = color;
			}
			else if (!string.IsNullOrEmpty(_form1.mainTextBox.Text))
			{
				_form1.mainTextBox.SelectionStart = absoluteIndex + myMatch.Index;
			}
		}
	}
}