using System.Collections.Generic;
using System.Linq;
using Mite;

namespace MiteyTimeTracking.Models.Mite
{
	public class CustomerModel
	{
		private readonly Dictionary<string, Customer> _customerMap = new Dictionary<string, Customer>();

		public CustomerModel(IList<Customer> customers)
		{
			var customers1 = new List<Customer>(customers);
			foreach (Customer customer in customers1)
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
