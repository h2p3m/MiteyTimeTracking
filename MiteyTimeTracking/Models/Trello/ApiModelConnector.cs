using TrelloNet;

namespace MiteyTimeTracking.Models.Trello
{
	public class ApiModelConnector
	{
		//TODO authorizing new user:
		//public Uri AuthorisationUrl { get { return _trello.GetAuthorizationUrl("MiteyTimeTracking", Scope.ReadWrite); } }
		//public string Authorize { set { _trello.Authorize(value); } }

		public CardModel Cards { get; private set; }

		public ApiModelConnector(string apiKey, string token)
		{
			ITrello trello = new TrelloNet.Trello(apiKey);
			trello.Authorize(token);

			Cards = new CardModel(trello);
		}
	}
}
