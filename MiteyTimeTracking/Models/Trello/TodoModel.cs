using System.Collections.Generic;
using MiteyTimeTracking.APIWrapper;
using TrelloNet;

namespace MiteyTimeTracking.Models.Trello
{
	public class TodoModel
	{
		public IMembers Members { get; private set; }
		public ICards Cards { get; set; }
		public List<List> Lists { get; private set; }
		

		public TodoModel()
		{
			var trello = new TrelloWrapper().Trello;

			Cards = trello.Cards;
			Members = trello.Members;

			Lists = new List<List>()
			{
				//DevBoard
				trello.Lists.WithId("53a7fe5129abc0630ab46061"), //Doing, Aufwand...
				trello.Lists.WithId("522d9c9b8aa9f3a06c00577c"), //TODO
				trello.Lists.WithId("54e1f299c06c1111fed1a7bd"), //Briefing, Analyse, ...
				trello.Lists.WithId("5417ec083f8e1d6ede575803"), //Doing

				//IssueBoard
				trello.Lists.WithId("54eaf781b5d30395d9a7d34b"), //TODO
				trello.Lists.WithId("54d4c1839d857d3e2741dc65") //Doing
			};
		}
	}
}
