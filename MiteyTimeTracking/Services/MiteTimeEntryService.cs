using MiteyTimeTracking.ViewModels;

namespace MiteyTimeTracking.Services
{
	class MiteTimeEntryService
	{
		public string TimeEntryText { get; private set; }

		public MiteTimeEntryService()
		{
			var evm = new MiteTimeEntryViewModel();
			TimeEntryText = string.Format("{0}\t<{1}>{2}'{3#'{4} {5}\n{6}", evm.TimePeriod.Item1, evm.Customer, 
				evm.Project, evm.Service, evm.CardIds, evm.text, evm.TimePeriod.Item2);
		}
	}
}
