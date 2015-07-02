using System.Collections.Generic;
using System.Linq;
using MiteyTimeTracking.Properties;
using MiteyTimeTracking.ViewModels;
using TrelloNet;

namespace MiteyTimeTracking.Services
{
	public class TrelloTodoService
	{
		private ITrello _trello;

		private List<List> _lists;

		public TrelloTodoService()
		{
			_trello = new Trello(Settings.Default.trelloAPIKey);
			_trello.Authorize(Settings.Default.trelloToken);

			_lists = new List<List>()
			{
				//DevBoard
				_trello.Lists.WithId("53a7fe5129abc0630ab46061"), //Doing, Aufwand...
				_trello.Lists.WithId("522d9c9b8aa9f3a06c00577c"), //TODO
				_trello.Lists.WithId("54e1f299c06c1111fed1a7bd"), //Briefing, Analyse, ...
				_trello.Lists.WithId("5417ec083f8e1d6ede575803"), //Doing

				//IssueBoard
				_trello.Lists.WithId("54eaf781b5d30395d9a7d34b"), //TODO
				_trello.Lists.WithId("54d4c1839d857d3e2741dc65") //Doing
			};
		}

		public TrelloTodoViewModel GetTodoLists()
		{
			var viewModel = new TrelloTodoViewModel();
			viewModel.TodoList = new List<KeyValuePair<string, List<Card>>>();

			foreach (var list in _lists)
			{
				string userId = _trello.Members.Me().Id;
				List<Card> doingCards = _trello.Cards.ForList(list).Where(
					c => c.IdMembers.Contains(userId)).ToList();

				viewModel.TodoList.Add(new KeyValuePair<string, List<Card>>(list.Name, doingCards));
			}

			return viewModel;
		}
	}
}