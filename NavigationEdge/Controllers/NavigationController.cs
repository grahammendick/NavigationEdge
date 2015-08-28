using NavigationEdge.Models;
using System;
using System.Collections.Generic;
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
			var people = new Data().SearchPeople((int)data.pageNumber);
			props["people"] = people.Select(p => new
			{
				id = p.Id,
				name = p.Name,
				dateOfBirth = p.DateOfBirth,
				email = p.Email,
				phone = p.Phone,
			});
			return props;
		}

		public IDictionary<string, object> GetPerson(dynamic data)
		{
			var props = new Dictionary<string, object>();
			var person = new Data().GetPerson((int)data.id);
			props["person"] = new
			{
				id = person.Id,
				name = person.Name,
				dateOfBirth = person.DateOfBirth,
				email = person.Email,
				phone = person.Phone,
			};
			return props;
		}
    }
}