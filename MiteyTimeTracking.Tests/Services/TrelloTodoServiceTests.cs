using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiteyTimeTracking.Services;
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
