using System.Collections.Generic;
using System.Linq;
using MiteyTimeTracking.Models.Trello;
using MiteyTimeTracking.ViewModels;
using TrelloNet;

namespace MiteyTimeTracking.Services
{
	public class TrelloTodoService
	{
		private TodoModel _todoModel;

		public TrelloTodoService()
		{
			_todoModel = new TodoModel();
		}

		public TrelloTodoViewModel GetTodoLists()
		{
			var viewModel = new TrelloTodoViewModel();
			viewModel.TodoList = new List<KeyValuePair<string, List<Card>>>();

			foreach (var list in _todoModel.Lists)
			{
				string userId = _todoModel.Members.Me().Id;
				List<Card> doingCards = _todoModel.Cards.ForList(list).Where(
					c => c.IdMembers.Contains(userId)).ToList();

				viewModel.TodoList.Add(new KeyValuePair<string, List<Card>>(list.Name, doingCards));
			}

			return viewModel;
		}
	}
}