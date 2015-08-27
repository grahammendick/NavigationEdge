using NavigationEdge.PropsProvider;
using System.Web.Mvc;

namespace NavigationEdge.Controllers
{
    public class DataController : Controller
    {
		public JsonResult SearchPeople(int pageNumber)
		{
			return this.Json(new Listing().GetProps(new { pageNumber = pageNumber }), JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetPerson(int id)
		{
			return this.Json(new Details().GetProps(new { id = id }), JsonRequestBehavior.AllowGet);
		}
	}
}