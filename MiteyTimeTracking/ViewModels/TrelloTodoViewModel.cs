using System.Collections.Generic;
using TrelloNet;

namespace MiteyTimeTracking.ViewModels
{
	public class TrelloTodoViewModel
	{
		public List<KeyValuePair<string, List<Card>>> TodoList { get; set; } 
	}
}