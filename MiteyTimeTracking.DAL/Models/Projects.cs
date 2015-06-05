using System.Collections.Generic;
using System.Linq;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Projects
	{
		private List<Project> _projects;

		public Projects(IList<Project> projects)
		{
			this._projects = new List<Project>(projects);
		}

		//TODO Filter zusätzlich auf Customer
		public List<string> GetMatchedProjectNames(string name, string customerName)
		{
			var foundProjects = _projects.FindAll(
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
