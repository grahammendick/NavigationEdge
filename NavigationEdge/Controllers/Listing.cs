using NavigationEdge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NavigationEdge.Controllers
{
	public class Listing : IPropsProvider
	{
		public async Task<IDictionary<string, object>> GetPropsAsync(dynamic data)
		{
			var props = new Dictionary<string, object>();
			var people = await new Data().SearchPeople((int)data.pageNumber);
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