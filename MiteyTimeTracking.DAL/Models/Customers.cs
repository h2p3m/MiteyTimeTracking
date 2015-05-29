using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Customers
	{
		private List<Customer> customers;

		public Customers(IList<Customer> customers)
		{
			customers = new List<Mite.Customer>(customers);
		}

		public List<string> GetCustomerNames(string name)
		{
			var foundCustomers = customers.FindAll(
				f => f.Name.ToUpper()
					.Contains(name.ToUpper()));

			List<string> result = new List<string>();
			foreach (var item in foundCustomers)
			{
				result.Add(item.Name);
			}

			return result;
		}
	}
}
