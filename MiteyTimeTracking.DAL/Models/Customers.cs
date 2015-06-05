using System.Collections.Generic;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Customers
	{
		private List<Customer> _customers;

		public Customers(IList<Customer> customers)
		{
			this._customers = new List<Customer>(customers);
		}

		public List<string> GetCustomerNames(string name)
		{
			var foundCustomers = _customers.FindAll(
				f => f.Name.ToUpper().Contains(
					name.ToUpper()));

			List<string> result = new List<string>();
			foreach (var item in foundCustomers)
			{
				result.Add(item.Name);
			}

			return result;
		}
	}
}
