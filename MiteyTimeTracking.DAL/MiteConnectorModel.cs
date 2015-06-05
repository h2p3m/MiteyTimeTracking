using System;
using System.Collections.Generic;
using Mite;
using MiteyTimeTracking.DAL.Models;

namespace MiteyTimeTracking.DAL
{
	public class MiteConnectorModel
	{
		public Customers Customers { get { return _customers; } }
		public Projects Projects { get { return _projects; } }
		public Services Services { get { return _services; } }

		private Customers _customers;
		private Projects _projects;
		private Services _services;

		public MiteConnectorModel()
		{
			var uri = new Uri("https://lambfra.mite.yo.lk");
			var miteConfiguration = new MiteConfiguration(uri, " 12439d9dafecb3c");

			using (IDataContext context = new MiteDataContext(miteConfiguration))
			{
				IList<Customer> miteCustomerList = context.GetAll<Customer>();
				IList<Project> miteProjectList = context.GetAll<Project>();
				IList<Service> miteServiceList = context.GetAll<Service>();

				_customers = new Customers(miteCustomerList);
				_projects = new Projects(miteProjectList);
				_services = new Services(miteServiceList);
			}
		}
	}
}
