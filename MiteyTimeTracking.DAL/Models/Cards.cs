using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrelloNet;


namespace MiteyTimeTracking.DAL.Models
{
	public class Cards
	{
		private readonly ITrello _trello;
		private Board _devBoardId;
		private Board _issueBoardId;

		public Cards(ITrello trello)
		{
			_trello = trello;
			_devBoardId = _trello.Boards.WithId("MpGmUVoZ");
			_issueBoardId = _trello.Boards.WithId("ICydUUxq");
		}

		public Dictionary<string, string> GetCardsByNumber(string cardNumber)
		{
			SearchFilter sf = new SearchFilter();
			sf.Boards = new List<IBoardId>() { _devBoardId, _issueBoardId };

			try
			{
				var result = _trello.Cards.Search(cardNumber, filter: sf);
				if (result != null)
				{
					List<Card> cards = result.ToList();
					return cards.Select(s => new {key=s.IdShort.ToString(), name=s.Name})
						.ToDictionary(d => d.key, d => d.name);
				}
				else throw new Exception();
			}

			catch (Exception)
			{
				return new Dictionary<string, string>();
			}
		}

	}
}