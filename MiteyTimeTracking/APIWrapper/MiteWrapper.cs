using System;
using System.Collections.Generic;
using Mite;
using MiteyTimeTracking.Models.Mite;
using MiteyTimeTracking.Properties;

namespace MiteyTimeTracking.APIWrapper
{
	public class MiteWrapper
	{
		public CustomerModel Customers { get; private set; }
		public ProjectModel Projects { get; private set; }
		public MiteServiceModel Services { get; private set; }

		public IDataContext Context { get; private set; }

		public MiteWrapper()
		{
			var uri = new Uri("https://lambfra.mite.yo.lk");
			var miteConfiguration = new MiteConfiguration(uri, Settings.Default.miteAPIKey);

			Context = new MiteDataContext(miteConfiguration);

			using (IDataContext context = new MiteDataContext(miteConfiguration))
			{
				IList<Customer> miteCustomerList = context.GetAll<Customer>();
				IList<Project> miteProjectList = context.GetAll<Project>();
				IList<Service> miteServiceList = context.GetAll<Service>();

				try
				{
					Customers = new CustomerModel(miteCustomerList);
					Projects = new ProjectModel(miteProjectList);
					Services = new MiteServiceModel(miteServiceList);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
	}
}
