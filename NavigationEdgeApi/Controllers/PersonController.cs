using NavigationEdgeApi.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace NavigationEdgeApi.Controllers
{
    public class PersonController : ApiController
    {
		public Person Get([ModelBinder] int id)
		{
			return new PersonRepository().People.First(p => p.Id == id);
		}
	}
}
