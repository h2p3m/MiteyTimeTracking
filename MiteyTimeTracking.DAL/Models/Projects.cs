using System;
using System.Collections.Generic;
using System.Linq;
using Mite;

namespace MiteyTimeTracking.DAL.Models
{
	public class Projects
	{
		private Dictionary<Project, string> _projectMap = new Dictionary<Project, string>();

		public Projects(IList<Project> projects)
		{
			var projects1 = new List<Project>(projects);
			foreach (Project project in projects1)
			{
				_projectMap.Add(project, project.Name.Split(' ')[0]);
			}
		}

		public Dictionary<string, string> GetMatchedProjectNames(string name, string customerName)
		{
			return _projectMap.Where(w => w.Value.ToUpper().Contains(name.ToUpper())
				&& w.Key.Customer.Name.ToUpper().Contains(customerName.ToUpper()))
				.Select(s => new {key=s.Key.Name, name=s.Value})
				.ToDictionary(d => d.key, d => d.name);
		}
	}
}
