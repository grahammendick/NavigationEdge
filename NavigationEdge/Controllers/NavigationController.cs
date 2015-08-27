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
			dynamic stateContext = await Navigator.GetStateContext(this.Request.Url.PathAndQuery);
			var state = stateContext.state;
			var data = stateContext.data;
			var pageNumber = data.pageNumber;
			return View();
        }
    }
}