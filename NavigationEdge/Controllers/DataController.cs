using NavigationEdge.PropsProvider;
using System.Web.Mvc;

namespace NavigationEdge.Controllers
{
    public class DataController : Controller
    {
		public JsonResult SearchPeople(int pageNumber)
		{
			return Json(new Listing().GetProps(new { pageNumber = pageNumber }), JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetPerson(int id)
		{
			return Json(new Details().GetProps(new { id = id }), JsonRequestBehavior.AllowGet);
		}
	}
}