using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiteyTimeTracking.Models.Mite;

namespace MiteyTimeTracking.Tests
{
	[TestClass()]
	public class MiteModelTests
	{
		[TestMethod()]
		public void MiteModelTest()
		{
			try
			{
				ApiModelConnector mm = new ApiModelConnector();
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}
	}
}
