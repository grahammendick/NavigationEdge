using NavigationEdge.Models;
using System.Collections.Generic;
using System.Linq;

namespace NavigationEdge.PropsProvider
{
	public class Listing : IPropsProvider
	{
		public IDictionary<string, object> GetProps(dynamic data)
		{
			var props = new Dictionary<string, object>();
			var people = new Data().SearchPeople((int)data.pageNumber);
			props["people"] = people.Select(p => new {
				id = p.Id,
				name = p.Name,
				dateOfBirth = p.DateOfBirth,
				email = p.Email,
				phone = p.Phone,
			});
			return props;
		}
	}
}