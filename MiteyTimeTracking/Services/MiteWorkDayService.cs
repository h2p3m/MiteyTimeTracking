using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiteyTimeTracking.ViewModels;

namespace MiteyTimeTracking.Services	
{
	public class MiteWorkDayService
	{
		//call by selecting a date
		public string GetTextByDate(DateTime date)
		{
			MiteTimeEntryService tes = new MiteTimeEntryService();
			MiteWorkDayViewModel workDayViewModel = new MiteWorkDayViewModel();

			return workDayViewModel.MiteTimeEntries.Aggregate(
				string.Empty, (current, entry) => current + entry.text);
		}

		public object GetSerializedMiteDay()
		{
			return null;
		}

		public object GetMiteWorkDay()
		{
			return null;
		}

		//call when text changes, e.g. by adding a timeEntry
		public void SetMiteDay()
		{
			
		}
	}
}
