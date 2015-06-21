using System.Collections.Generic;
using System.Linq;
using Mite;

namespace MiteyTimeTracking.Models.Mite
{
	public class ProjectModel
	{
		private readonly Dictionary<Project, string> _projectMap = new Dictionary<Project, string>();

		public ProjectModel(IList<Project> projects)
		{
			var projects1 = new List<Project>(projects);
			foreach (Project project in projects1)
			{
				_projectMap.Add(project, project.Name.Split(' ')[0]);
			}
		}

		public Dictionary<string, string> GetMatchedProjectNames(string name, string customerName)
		{
			if (string.IsNullOrWhiteSpace(customerName))
			{
				return null;
			}
			return _projectMap.Where(w => w.Value.ToUpper().Contains(name.ToUpper())
				&& w.Key.Customer.Name.ToUpper().Contains(customerName.ToUpper()))
				.Select(s => new { key = s.Key, name = s.Value })
				.ToDictionary(d => d.name, d => d.key.Name);
		}
	}
}
