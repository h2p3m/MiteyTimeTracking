using System;
using System.Drawing;
using MiteyTimeTracking.Services;

namespace MiteyTimeTracking.Core
{
	public class Controller
	{
		private Form1 _form1;

		public Controller(Form1 form1)
		{
			_form1 = form1;
		}

		/// <summary>
		/// check for selected date if MiteEntry
		/// is serialized to filesystem and/or 
		/// exists on mite webservice
		/// </summary>
		public void InitalizeMiteDay()
		{
			MiteWorkDayService mdService = new MiteWorkDayService();

			if (mdService.GetSerializedMiteDay() == null || mdService.GetMiteWorkDay() == null)
			{
				_form1.RichTextBox1.Text = DateTime.Now.ToShortDateString() + @" ";
				_form1.RichTextBox1.Find(DateTime.Now.ToShortDateString());
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