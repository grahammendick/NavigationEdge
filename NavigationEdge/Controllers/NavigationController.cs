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
			var state = stateContext.state;
			var data = stateContext.data;
			var pageNumber = data.pageNumber;
			var people = new List<Person>();
			people.Add(new Person { Id = 1, name = "test " });
			var dict = new Dictionary<string, object>();
			dict["people"] = people;
			var content = await React.Render(this.Request.Url.PathAndQuery, dict);
			return View(content);
        }
    }
}