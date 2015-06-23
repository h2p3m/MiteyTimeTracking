using System.Collections.Generic;
using MiteyTimeTracking.DAL;
using MiteyTimeTracking.Properties;
using TrelloNet;

namespace MiteyTimeTracking.Services
{
	public class TrelloTodoService
	{
		private ITrello _trello;
		private Board _devBoardId;
		private Board _issueBoardId;

		public TrelloTodoService()
		{
			_trello = new Trello(Settings.Default.trelloAPIKey);
			_trello.Authorize(Settings.Default.trelloToken);
			_devBoardId = _trello.Boards.WithId("MpGmUVoZ");
			_issueBoardId = _trello.Boards.WithId("ICydUUxq");
			
			var sf = new SearchFilter
			{
				Boards = new List<IBoardId>() {_devBoardId, _issueBoardId}
			};

			_trello.Lists.
		}
	}
}