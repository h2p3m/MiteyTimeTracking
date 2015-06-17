using System;
using System.Collections.Generic;
using Mite;

namespace MiteyTimeTracking.ViewModels
{
	[Serializable]
	public class MiteTimeEntryViewModel
	{
		public Tuple<DateTime, DateTime> TimePeriod { get; set; }
		public Customer Customer { get; set; }
		public Project Project { get; set; }
		public Service Service { get; set; }
		public List<int> CardIds { get; set; }
		public string text { get; set; }
	}
}
