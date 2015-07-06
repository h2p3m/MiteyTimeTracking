using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiteyTimeTracking.APIWrapper;

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
				MiteWrapper mm = new MiteWrapper();
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}
	}
}
