using NavigationEdgeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace NavigationEdgeApi.Controllers
{
    public class PersonController : ApiController
    {
		public Person Get([ModelBinder] int id)
		{
			return new Data().GetPerson(id);
		}
	}
}
