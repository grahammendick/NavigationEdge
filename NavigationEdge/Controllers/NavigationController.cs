using NavigationEdge.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NavigationEdge.Controllers
{
    public class NavigationController : Controller
    {
		public async Task<ActionResult> Index()
        {
			dynamic stateContext = await Navigation.GetContext(Request.Url.PathAndQuery);
			var propsProviderType = Type.GetType("NavigationEdge.Controllers." + (string)stateContext.propsProvider);
			var propsProvider = (IPropsProvider)Activator.CreateInstance(propsProviderType);
			var props = propsProvider.GetProps(stateContext.data);
			var content = await React.Render(Request.Url.PathAndQuery, props);
			if (Request.AcceptTypes.Contains("application/json"))
				return Json(props, JsonRequestBehavior.AllowGet);
			else
				return View(new Component{ Props = props, Content = content });
        }
    }
}