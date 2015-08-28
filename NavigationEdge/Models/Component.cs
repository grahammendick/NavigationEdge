using System.Collections.Generic;

namespace NavigationEdge.Models
{
	public class Component
	{
		public string Content { get; set; }
		public IDictionary<string, object> Props { get; set; }
	}
}