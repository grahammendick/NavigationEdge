using NavigationEdgeApi.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace NavigationEdgeApi.Controllers
{
    public class PeopleController : ApiController
    {
		public IEnumerable<Person> Get([ModelBinder] int pageNumber)
		{
			return new Data().SearchPeople(pageNumber);
		}
	}
}
