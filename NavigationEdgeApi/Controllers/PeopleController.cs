using NavigationEdgeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace NavigationEdgeApi.Controllers
{
    public class PeopleController : ApiController
    {
		public IEnumerable<Person> Get([ModelBinder] int pageNumber)
		{
			return new PersonRepository().People.Skip((pageNumber - 1) * 10).Take(10);
		}
	}
}
