using MiteyTimeTracking.Properties;
using TrelloNet;

namespace MiteyTimeTracking.APIWrapper
{
	public class TrelloWrapper
	{
		public ITrello Trello { get; private set; }

		public TrelloWrapper()
		{
			var ApiKey = Settings.Default.trelloAPIKey;
			var Token = Settings.Default.trelloToken;

			Trello = new Trello(ApiKey);
			Trello.Authorize(Token);
		}

		//----------------------------------------------------------------
		//sadly the beautiful manatee.trello library hangs on any api call
		//----------------------------------------------------------------
		//public static void InitManateeTrello()
		//{
		//	var serializer = new ManateeSerializer();
		//	TrelloConfiguration.Serializer = serializer;

		//	TrelloConfiguration.Deserializer = serializer;
		//	TrelloConfiguration.JsonFactory = new ManateeFactory(); 
		//	TrelloConfiguration.RestClientProvider = new RestSharpClientProvider();

		//	TrelloAuthorization.Default.AppKey = ApiKey;
		//	TrelloAuthorization.Default.UserToken = Token;

		//	var boardId = "rKMvSTG8";

		//	string query = "trello";
		//	var board = new Board(boardId);
		//	var results = new Search(query, 100, SearchModelType.Cards, new IQuer	yable[] { board });
		//	Console.WriteLine(Enumerable.Count(results.Cards));

		//	//Console.WriteLine(board);
		//	//foreach (var card in board.Cards)
		//	//{
		//	//	Console.WriteLine(card);
		//	//}
		//}
	}
}
