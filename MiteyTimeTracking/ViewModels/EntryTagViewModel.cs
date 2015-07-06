using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiteyTimeTracking.Interfaces;

namespace MiteyTimeTracking.ViewModels
{
	public class EntryTagViewModel : IEntryTagViewModel
	{
		public Dictionary<string, string> AllTags { get; set; }
	}
}
