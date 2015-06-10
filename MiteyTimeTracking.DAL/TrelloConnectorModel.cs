using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloNet;
using TrelloNet.Internal;
using System.Configuration;
using MiteyTimeTracking.DAL.Models;

namespace MiteyTimeTracking.DAL
{
	public class TrelloConnectorModel
	{
		public Uri AuthorisationUrl { get { return _trello.GetAuthorizationUrl("MiteyTimeTracking", Scope.ReadWrite); } }
		public string Authorize { set { _trello.Authorize(value); } }
		public Cards Cards { get { return _cards; } private set { _cards = value; } }
		private Cards _cards;

		private readonly ITrello _trello;

		public TrelloConnectorModel(string apiKey, string token)
		{
			_trello = new Trello(apiKey);
			try
			{
				_trello.Authorize(token);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			Cards = new Cards(_trello);
		}

	}
}
