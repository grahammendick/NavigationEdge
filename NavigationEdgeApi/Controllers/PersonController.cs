using NavigationEdgeApi.Models;
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
