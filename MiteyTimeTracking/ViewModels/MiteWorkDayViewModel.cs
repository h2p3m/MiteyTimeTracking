using System;
using System.Collections.Generic;

namespace MiteyTimeTracking.ViewModels
{
	[Serializable]
	public class MiteWorkDayViewModel
	{
		public List<MiteTimeEntryViewModel> MiteTimeEntries { get; set; }
	}
}
