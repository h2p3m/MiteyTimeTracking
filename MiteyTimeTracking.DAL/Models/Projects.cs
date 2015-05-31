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
		public List<string> GetMatchedProjectNames(string name)
		{
			var foundProjects = projects.FindAll(
				f => f.Name.ToUpper().Contains(
					name.ToUpper()));

			List<string> result = new List<string>();
			foreach (var item in foundProjects)
			{
				result.Add(item.Name);
			}

			return result;
		}
	}
}
