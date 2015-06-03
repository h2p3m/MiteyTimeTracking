using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Services
	{
		private List<Service> services;

		public Services(IList<Service> services)
		{
			this.services = new List<Service>(services);
		}

		public List<string> GetMachedServiceNames(string name)
		{
			var foundServices = services.FindAll(
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
