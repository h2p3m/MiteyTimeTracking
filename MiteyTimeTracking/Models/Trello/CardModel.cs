using System;
using System.Collections.Generic;
using System.Linq;
using MiteyTimeTracking.APIWrapper;
using TrelloNet;

namespace MiteyTimeTracking.Models.Trello
{
	public class CardModel
	{
		private readonly ITrello _trello;

		private readonly Board _devBoardId;
		private readonly Board _issueBoardId;

		public CardModel()
		{
			var _trello = new TrelloWrapper().Trello;

			_trello.Members.Me();

			_devBoardId = _trello.Boards.WithId("MpGmUVoZ");
			_issueBoardId = _trello.Boards.WithId("ICydUUxq");
		}


		public Dictionary<string, string> GetCardsByNumber(string cardNumber)
		{
			var sf = new SearchFilter
			{
				Boards = new List<IBoardId>() {_devBoardId, _issueBoardId}
			};

			try
			{
				var result = _trello.Cards.Search(cardNumber, filter: sf);
				if (result == null) throw new Exception();
				
				var cards = result.ToList();
				var c = cards.Select(s => new {key=s.IdShort.ToString(), name=s.Name})
					.ToDictionary(d => d.key, d => d.name);
				return c;
			}

			catch (Exception)
			{
				return new Dictionary<string, string>();
			}
		}

	}
}