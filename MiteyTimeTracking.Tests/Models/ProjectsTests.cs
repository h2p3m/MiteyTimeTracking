using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiteyTimeTracking.APIWrapper;

namespace MiteyTimeTracking.DAL.Models.Tests
{
	[TestClass()]
	public class ProjectsTests
	{
		[TestMethod()]
		public void GetMatchedProjectNamesTest()
		{
			MiteWrapper mite = new MiteWrapper();
			try
			{
				mite.Projects.GetMatchedProjectNames(">r", "hht");
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}
	}
}
