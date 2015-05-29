using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mite;
using MiteyTimeTracking.DAL.Models;

namespace MiteyTimeTracking.DAL
{
	public class MiteConnectorModel
	{
		public Customers Customers { get { return customers; } }
		public Projects Projects { get { return projects; } }
		public Services Services { get { return services; } }

		private Customers customers;
		private Projects projects;
		private Services services;

		public MiteConnectorModel()
		{
			var uri = new Uri("https://lambfra.mite.yo.lk");
			var miteConfiguration = new MiteConfiguration(uri, " 12439d9dafecb3c");

			using (IDataContext context = new MiteDataContext(miteConfiguration))
			{
				IList<Customer> miteCustomerList = context.GetAll<Customer>();
				IList<Project> miteProjectList = context.GetAll<Project>();
				IList<Service> miteServiceList = context.GetAll<Service>();

				customers = new Customers(miteCustomerList);
				projects = new Projects(miteProjectList);
				services = new Services(miteServiceList);
			}
		}
	}
}
