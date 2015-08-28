using NavigationEdge.Models;
using NavigationEdge.PropsProvider;
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
			var propsProviderType = Type.GetType("NavigationEdge.PropsProvider." + (string)stateContext.propsProvider);
			var propsProvider = (IPropsProvider)Activator.CreateInstance(propsProviderType);
			var props = propsProvider.GetProps(stateContext.data);
			var content = await React.Render(Request.Url.PathAndQuery, props);
			if (Request.AcceptTypes.Contains("application/json"))
				return Json(props, JsonRequestBehavior.AllowGet);
			var component = new Component {
				Props = new JavaScriptSerializer().Serialize(props),
				Content = content
			};
			return View(component);
        }
    }
}