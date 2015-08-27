using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NavigationEdge
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "searchPeople",
				url: "data/people/{pageNumber}",
				defaults: new { controller = "Data", action = "SearchPeople" }
			);

			routes.MapRoute(
				name: "getPerson",
				url: "data/person/{id}",
				defaults: new { controller = "Data", action = "GetPerson" }
			);

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "Navigation", action = "Index" }
            );
        }
    }
}
