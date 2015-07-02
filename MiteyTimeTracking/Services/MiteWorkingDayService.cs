using System;
using System.IO;
using System.Linq;
using MiteyTimeTracking.Helpers;
using MiteyTimeTracking.ViewModels;
using Newtonsoft.Json;

namespace MiteyTimeTracking.Services
{
	public class MiteWorkingDayService
	{
		private readonly FileHelpers _fileHelpers;
		public MiteWorkDayViewModel MiteWorkingDayViewModel { get; private set; }

		public MiteWorkingDayService(DateTime today)
		{
			_fileHelpers = new FileHelpers();
			MiteWorkingDayViewModel = GetViewModel(today);
		}

		private MiteWorkDayViewModel GetViewModel(DateTime today)
		{
			var dailyPath = FileHelpers.GetDailyPath(today);
			var fullFileName = dailyPath + @"\" + today.Date.ToString("yyyy-MM-dd") + @".json";

			//TODO: check for online mite entries as well
			if (File.Exists(fullFileName))
			{
				var file = File.ReadAllText(fullFileName);
				return JsonConvert.DeserializeObject<MiteWorkDayViewModel>(file);
			}
			var miteWorkDayViewModel = new MiteWorkDayViewModel
			{
				Date = today
			};
			_fileHelpers.SerializeObjectToFile(fullFileName, dailyPath, miteWorkDayViewModel);
			return miteWorkDayViewModel;
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

		public string GetWorkDayEntriesAsText()
		{
			if (MiteWorkingDayViewModel != null)
			{
				return MiteWorkingDayViewModel.MiteTimeEntries.Aggregate(
					string.Empty, (current, entry) => current + entry.text);
			}
			else
			{
				throw new Exception("Kein Eintrag vorhanden.");
			}
		}
	}
}
