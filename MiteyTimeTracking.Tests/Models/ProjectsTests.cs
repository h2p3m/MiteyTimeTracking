using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiteyTimeTracking.Models.Mite;

namespace MiteyTimeTracking.DAL.Models.Tests
{
	[TestClass()]
	public class ProjectsTests
	{
		[TestMethod()]
		public void GetMatchedProjectNamesTest()
		{
			ApiModelConnector mite = new ApiModelConnector();
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
