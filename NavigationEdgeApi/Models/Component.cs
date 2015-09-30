using System.Collections.Generic;

namespace NavigationEdgeApi.Models
{
	public class Component
	{
		public string Content { get; set; }
		public IDictionary<string, object> Props { get; set; }
	}
}