using System;
using MiteyTimeTracking.Models.Trello;
using TrelloNet;

namespace MiteyTimeTracking.DAL
{
	public class TrelloModel
	{
		//TODO authorizing new user:
		//public Uri AuthorisationUrl { get { return _trello.GetAuthorizationUrl("MiteyTimeTracking", Scope.ReadWrite); } }
		//public string Authorize { set { _trello.Authorize(value); } }

		public CardModel Cards { get; private set; }

		public TrelloModel(string apiKey, string token)
		{
			ITrello trello = new Trello(apiKey);
			trello.Authorize(token);

			Cards = new CardModel(trello);
		}

	}
}
