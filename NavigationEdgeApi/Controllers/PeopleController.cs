using NavigationEdgeApi.Models;
using NavigationEdgeApi.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace NavigationEdgeApi.Controllers
{
    public class PeopleController : ApiController
    {
		public Dictionary<string, IEnumerable<Person>> Get([ModelBinder] int pageNumber)
		{
			var people = new Data().SearchPeople(pageNumber);
			return new Dictionary<string, IEnumerable<Person>>() { { "people", people } };
		}
	}
}
