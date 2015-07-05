using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiteyTimeTracking.APIWrapper.Tests
{
	[TestClass()]
	public class TrelloWrapperTests
	{
		[TestMethod()]
		public void InitTrelloTest()
		{
			try
			{
				//out couse manatee.trello doesn't work
				//var wrapper = new TrelloWrapper();
				//TrelloWrapper.InitManateeTrello();

				//var boardId = "MpGmUVoZ";
				//var board = new Board(boardId);
				//Console.WriteLine(board);
				//foreach (var card in board.Cards)
				//{
				//	Console.WriteLine(card);
				//}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			Assert.Fail();
		}
	}
}
