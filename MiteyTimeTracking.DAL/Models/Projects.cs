using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Projects
	{
		private List<Project> projects;

		public Projects(IList<Project> projects)
		{
			this.projects = new List<Project>(projects);
		}

		//TODO Filter zusätzlich auf Customer
		public List<string> GetMatchedProjectNames(string name, string customerName)
		{
			var foundProjects = projects.FindAll(
				f => f.Name.ToUpper().Contains(name.ToUpper()));
			foundProjects = foundProjects.FindAll(
				f => f.Customer.Name.ToUpper().Contains(customerName.ToUpper()));

			List<string> result = new List<string>();
			foreach (var item in foundProjects)
			{
				result.Add(item.Name.Replace(" ", string.Empty));
			}

			return result.Distinct().ToList();
		}
	}
}
