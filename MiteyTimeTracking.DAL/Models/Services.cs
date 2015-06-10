using System;
using System.Collections.Generic;
using System.Linq;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Services
	{
		private List<Service> _services;

		public Services(IList<Service> services)
		{
			this._services = new List<Service>(services);
		}

		public Dictionary<string, string> GetMachedServiceNames(string name)
		{
			return _services.Where(w => w.Name.ToUpper().Contains(name.ToUpper()))
				.Select(s => new {key = s.Name.Replace(" ", String.Empty), name = s.Name})
				.ToDictionary(d => d.key, d => d.name);
		}
	}
}
