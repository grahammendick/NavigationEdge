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
		public async Task<HttpResponseMessage> Get([ModelBinder] int pageNumber)
		{
			var people = new Data().SearchPeople(pageNumber);
			var props = new Dictionary<string, object>() { { "people", people } };
			var content = await React.Render(Request.RequestUri.PathAndQuery, props);
			return null;
		}
	}
}
