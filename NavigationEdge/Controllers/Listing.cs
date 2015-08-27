using NavigationEdge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavigationEdge.Controllers
{
	public class Listing : IPropsProvider
	{
		public void SetProps(Dictionary<string, object> props, dynamic data)
		{
			props["people"] = new Data().SearchPeople((int)data.pageNumber).Select(p => new
			{
				id = p.Id,
				name = p.Name,
				dateOfBirth = p.DateOfBirth,
				email = p.Email,
				phone = p.Phone,
			});
		}
	}
}