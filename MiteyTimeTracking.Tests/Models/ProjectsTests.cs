using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiteyTimeTracking.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MiteyTimeTracking.DAL.Models.Tests
{
	[TestClass()]
	public class ProjectsTests
	{
		[TestMethod()]
		public void GetMatchedProjectNamesTest()
		{
			MiteApiAbscrator mite = new MiteApiAbscrator();
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
