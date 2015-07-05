using System;
using System.Collections.Generic;
using Mite;
using MiteyTimeTracking.Models.Mite;

namespace MiteyTimeTracking.APIWrapper
{
	public class ApiModelConnector
	{
		public CustomerModel Customers { get; private set; }
		public ProjectModel Projects { get; private set; }
		public MiteServiceModel Services { get; private set; }

		public ApiModelConnector()
		{
			var uri = new Uri("https://lambfra.mite.yo.lk");
			var miteConfiguration = new MiteConfiguration(uri, " 12439d9dafecb3c");

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

				Condition c = new Condition();
				c.Value = 51420;
				c.Property = "user-id";

				Condition c2 = new Condition();
				c2.Value = "today";
				c2.Property = "at";

				QueryExpression qe = new QueryExpression();
				qe.Conditions.Add(c);
				qe.Conditions.Add(c2);

				var entries = context.GetByCriteria<TimeEntry>(qe);
				//GetAll<Mite.TimeEntry>();
				//.Where(w => w.User.Id == 51420 
				//	&& w.CreatedOn >= new DateTime(2015, 4, 1));
				//var user = context.GetAll<Mite.User>();
			}
		}
	}
}
