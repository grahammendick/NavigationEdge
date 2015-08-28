using NavigationEdge.Models;
using System.Collections.Generic;

namespace NavigationEdge.Controllers
{
	public class Details : IPropsProvider
	{
		public IDictionary<string, object> GetProps(dynamic data)
		{
			var props = new Dictionary<string, object>();
			var person = new Data().GetPerson((int)data.id);
			props["person"] = new {
				id = person.Id,
				name = person.Name,
				dateOfBirth = person.DateOfBirth,
				email = person.Email,
				phone = person.Phone,
			};
			return props;
		}
	}
}