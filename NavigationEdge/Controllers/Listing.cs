using NavigationEdge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavigationEdge.Controllers
{
	public class Listing : IPropsProvider
	{
		public IDictionary<string, object> GetProps(dynamic data)
		{
			var props = new Dictionary<string, object>();
			props["people"] = new Data().SearchPeople((int)data.pageNumber).Select(p => new
			{
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