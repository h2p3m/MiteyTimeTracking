using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using MiteyTimeTracking.Services;
using MiteyTimeTracking.ViewModels;
using Newtonsoft.Json;

namespace MiteyTimeTracking.Helpers
{
	public class FileHelpers
	{
		public static string GetDailyPath(DateTime today)
		{
			Assembly assembly = typeof (MiteWorkingDayService).Assembly;
			var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var dailyPath = Path.Combine(docFolder
				, assembly.FullName.Split(',')[0]
				, today.Date.Year.ToString()
				, today.Date.ToString("MM - MMMM", CultureInfo.CurrentCulture));
			return dailyPath;
		}

		public void SerializeObjectToFile(string fullFileName, string dailyPath
			, MiteWorkDayViewModel miteWorkDayViewModel)
		{
			var jObject = JsonConvert.SerializeObject(miteWorkDayViewModel);
			if (!Directory.Exists(dailyPath))
				Directory.CreateDirectory(dailyPath);
			File.WriteAllText(fullFileName, jObject);
		}
	}
}