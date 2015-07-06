using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mite;
using MiteyTimeTracking.APIWrapper;
using MiteyTimeTracking.Enums;
using MiteyTimeTracking.Models.Mite;
using MiteyTimeTracking.Models.Trello;
using MiteyTimeTracking.ViewModels;

namespace MiteyTimeTracking.Services
{
	public class EntryTagService
	{
		public CustomerModel Customers { get; private set; }
		public ProjectModel Projects { get; private set; }
		public MiteServiceModel Services { get; private set; }
		public CardModel CardModel { get; private set; }

		public EntryTagService()
		{
			CardModel = new CardModel();
			var miteWrapper = new MiteWrapper();

			using (IDataContext context = miteWrapper.Context)
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

		public EntryTagViewModel GetEntryTagViewModel(string tagName, TagType currenTagType, string activeCustomer)
		{
			var viewModel = new EntryTagViewModel();
			switch (currenTagType)
			{
				case TagType.Customer:
					viewModel.AllTags = Customers.GetCustomerNames(tagName);
					break;
				case TagType.Project:
					if (string.IsNullOrWhiteSpace(activeCustomer))
						throw new Exception("No active Customer set.\n\n"
							+ "Open a new issue here: https://github.com/h2p3m/MiteyTimeTracking/issues/new\n"
							+ "Or look for an active one: https://github.com/h2p3m/MiteyTimeTracking/issues");
					viewModel.AllTags = Projects.GetMatchedProjectNames(tagName, activeCustomer);
					break;
				case TagType.Service:
					viewModel.AllTags = Services.GetMachedServiceNames(tagName)
						.Reverse().ToDictionary(d => d.Key, d => d.Value);
					break;
				case TagType.Task:
					viewModel.AllTags = CardModel.GetCardsByNumber(tagName);
					break;
			}
			return viewModel;
		}
	}
}
