using NavigationEdge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NavigationEdge.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
		public async Task<ActionResult> Index()
        {
			dynamic stateContext = await Navigation.GetContext(this.Request.Url.PathAndQuery);
			var provider = (string) stateContext.provider;
			var data = stateContext.data;
			var propsProvider = (IPropsProvider) Activator.CreateInstance(Type.GetType("NavigationEdge.Controllers." + provider));
			var props = new Dictionary<string, object>();
			propsProvider.SetProps(props, data);
			var content = await React.Render(this.Request.Url.PathAndQuery, props);
			return View(content);
        }
    }
}