using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Customers
	{
		private List<Customer> _customers;
		private Dictionary<string, Customer> _customerMap = new Dictionary<string, Customer>();

		public Customers(IList<Customer> customers)
		{
			_customers = new List<Customer>(customers);
			foreach (Customer customer in _customers)
			{
				_customerMap.Add(customer.Name.Split(' ')[0], customer);
			}
		}

		public Dictionary<string, string> GetCustomerNames(string name)
		{
			return _customerMap.Where(w => w.Key.ToUpper().Contains(name.ToUpper()))
				.Select(s => new { key = s.Key, name = s.Value.Name })
				.ToDictionary(d => d.key, d => d.name);
		}
	}
}
