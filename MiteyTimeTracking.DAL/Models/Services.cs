using System.Collections.Generic;
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

		public List<string> GetMachedServiceNames(string name)
		{
			var foundServices = _services.FindAll(
				f => f.Name.ToUpper().Contains(
					name.ToUpper()));

			List<string> result = new List<string>();
			foreach (var item in foundServices)
			{
				result.Add(item.Name.Replace(" ", string.Empty));
			}

			return result;
		}
	}
}
