using System;
using System.Drawing;
using MiteyTimeTracking.Services;
using MiteyTimeTracking.ViewModels;

namespace MiteyTimeTracking.Core
{
	public class Controller
	{
		private Form1 _form1;
		private DateTime _today;

		public Controller(Form1 form1)
		{
			_form1 = form1;
			_today = _form1.today;
		}

		/// <summary>
		/// check for selected date if MiteEntry
		/// is serialized to filesystem and/or 
		/// exists on mite webservice
		/// </summary>
		public void InitalizeMiteDay()
		{
			MiteWorkDayService mdService = new MiteWorkDayService(_form1.today);

			if (mdService.GetSerializedMiteDay() == null || mdService.GetMiteWorkDay() == null)
			{
				_form1.RichTextBox1.Text = mdService.MiteWorkDayViewModel.Date.ToShortDateString() + @" ";
				_form1.RichTextBox1.Find(_today.ToShortDateString());
				_form1.RichTextBox1.SelectionColor = Color.RoyalBlue;
				_form1.RichTextBox1.SelectionStart = _form1.RichTextBox1.Text.Length;
				_form1.RichTextBox1.SelectionColor = Color.Black;
			}
		}

		public void SetTrelloTodo()
		{
			TrelloTodoService ttService = new TrelloTodoService();
			TrelloTodoViewModel viewModel = ttService.GetTodoLists();

			var currentFont = _form1.RichTextBox3.SelectionFont;
			_form1.RichTextBox3.Text = String.Empty;

			foreach (var list in viewModel.TodoList)
			{
				ToggleBold(10);
				_form1.RichTextBox3.AppendText(list.Key);
				ToggleBold(currentFont.Size);

				foreach (var card in list.Value)
				{
					_form1.RichTextBox3.AppendText(Environment.NewLine);
					_form1.RichTextBox3.AppendText(">> " + card.Name);
					ToggleBold(0);
					_form1.RichTextBox3.AppendText(" #" + card.IdShort);
					ToggleBold(0);
				}

				_form1.RichTextBox3.AppendText(Environment.NewLine);
				_form1.RichTextBox3.AppendText(Environment.NewLine);
			}
		}

		private void ToggleBold(float size)
		{
			var currentFont = _form1.RichTextBox3.SelectionFont;
			FontStyle newFontStyle;
			newFontStyle = _form1.RichTextBox3.SelectionFont.Bold ? FontStyle.Regular : FontStyle.Bold;

			_form1.RichTextBox3.SelectionFont = new Font(
				currentFont.FontFamily,
				Math.Abs(size) < 1 ? currentFont.Size : size,
				newFontStyle
				);
		}
	}
}