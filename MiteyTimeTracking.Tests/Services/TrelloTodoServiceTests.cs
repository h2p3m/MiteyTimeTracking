using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiteyTimeTracking.Services.Tests
{
	[TestClass()]
	public class TrelloTodoServiceTests
	{
		[TestMethod()]
		public void TrelloTodoServiceTest()
		{
			try
			{
				TrelloTodoService tts = new TrelloTodoService();
				var list = tts.GetTodoLists();
			}
			catch (Exception ex)
			{
				Assert.Fail();
				throw ex;
			}
		}
	}
}
