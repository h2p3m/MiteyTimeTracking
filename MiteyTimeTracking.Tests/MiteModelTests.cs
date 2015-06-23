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
				MiteApiAbscrator mm = new MiteApiAbscrator();
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}
	}
}
