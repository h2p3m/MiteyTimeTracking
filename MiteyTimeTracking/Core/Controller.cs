using System;
using System.Drawing;
using MiteyTimeTracking.Services;

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
			else
			{
			}
		}
	}
}