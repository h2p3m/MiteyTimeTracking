using System;
using System.Collections.Generic;

namespace MiteyTimeTracking.ViewModels
{
	public class MiteWorkDayViewModel
	{
		public DateTime Date { get; set; }
		public List<MiteTimeEntryViewModel> MiteTimeEntries { get; set; }
	}
}
