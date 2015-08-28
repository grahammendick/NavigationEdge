using NavigationEdge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NavigationEdge.Controllers
{
    public class NavigationController : Controller
    {
		public async Task<ActionResult> Index()
        {
			dynamic stateContext = await Navigation.GetContext(Request.Url.PathAndQuery);
			var propsMethod = this.GetType().GetMethod((string)stateContext.action);
			var props = new Dictionary<string, object>();
			propsMethod.Invoke(this, new object[] { props, stateContext.data });
			if (Request.ContentType == "application/json")
			{
				return Json(props, JsonRequestBehavior.AllowGet);
			}
			else
			{
				var content = (string) await React.Render(Request.Url.PathAndQuery, props);
				return View(new Component { Props = props, Content = content });
			}
        }

		public void SearchPeople(Dictionary<string, object> props, dynamic data)
		{
			props["people"] = new Data().SearchPeople((int)data.pageNumber);
		}

		public void GetPerson(Dictionary<string, object> props, dynamic data)
		{
			props["person"] = new Data().GetPerson((int)data.id);
		}
    }
}