using System;
using System.Collections.Generic;
using System.Linq;
using Mite;

namespace MiteyTimeTracking.Models.Mite
{
	public class MiteServiceModel
	{
		private readonly List<Service> _services;

		public MiteServiceModel(IList<Service> services)
		{
			_services = new List<Service>(services);
		}

		public Dictionary<string, string> GetMachedServiceNames(string name)
		{
			return _services.Where(w => w.Name.ToUpper().Contains(name.ToUpper()))
				.Select(s => new {key = s.Name.Replace(" ", String.Empty), name = s.Name})
				.ToDictionary(d => d.key, d => d.name);
		}
	}
}
