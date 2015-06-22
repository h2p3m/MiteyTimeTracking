using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiteyTimeTracking.DAL;

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
				MiteModel mm = new MiteModel();
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}
	}
}
