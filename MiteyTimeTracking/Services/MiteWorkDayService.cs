using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiteyTimeTracking.ViewModels;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using MiteyTimeTracking.Properties;

namespace MiteyTimeTracking.Services
{
	public class MiteWorkDayService
	{
		public MiteWorkDayViewModel MiteWorkDayViewModel { get; private set; }
		private string _fullFileName;
		private string _dailyPath;

		public MiteWorkDayService(DateTime today)
		{
			Assembly assembly = typeof(MiteWorkDayService).Assembly;
			var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			_dailyPath = Path.Combine(docFolder
				, assembly.FullName.Split(',')[0]
				, today.Date.Year.ToString()
				, today.Date.ToString("MM - MMMM", CultureInfo.CurrentCulture));
			_fullFileName = _dailyPath + @"\" + today.Date.ToString("yyyy-MM-dd") + @".json";

			//TODO: check for online mite entries as well
			if (File.Exists(_fullFileName))
			{
				var file = File.ReadAllText(_fullFileName);
				MiteWorkDayViewModel = JsonConvert.DeserializeObject<MiteWorkDayViewModel>(file);
			}
			else
			{
				MiteWorkDayViewModel = new MiteWorkDayViewModel()
				{
					Date = today
				};
				SafeObjectToFile();
			}
		}

		public string GetWorkDayEntriesAsText()
		{
			if (MiteWorkDayViewModel != null)
			{
				return MiteWorkDayViewModel.MiteTimeEntries.Aggregate(
					string.Empty, (current, entry) => current + entry.text);
			}
			else
			{
				throw new Exception("Kein Eintrag vorhanden.");
			}
		}

		public void SafeObjectToFile()
		{
			var jObject = JsonConvert.SerializeObject(MiteWorkDayViewModel);
			if (!Directory.Exists(_dailyPath))
				Directory.CreateDirectory(_dailyPath);
			File.WriteAllText(_fullFileName, jObject);
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
