using NavigationEdge.Models;
using System.Collections.Generic;
using System.Linq;
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
			var props = (IDictionary<string, object>) propsMethod.Invoke(this, new object[] { stateContext.data });
			var content = (string) await React.Render(Request.Url.PathAndQuery, props);
			if (Request.AcceptTypes.Contains("application/json"))
				return Json(props, JsonRequestBehavior.AllowGet);
			else
				return View(new Component{ Props = props, Content = content });
        }

		public IDictionary<string, object> SearchPeople(dynamic data)
		{
			var props = new Dictionary<string, object>();
			props["people"] = new Data().SearchPeople((int)data.pageNumber);
			return props;
		}

		public IDictionary<string, object> GetPerson(dynamic data)
		{
			var props = new Dictionary<string, object>();
			props["person"] = new Data().GetPerson((int)data.id);
			return props;
		}
    }
}