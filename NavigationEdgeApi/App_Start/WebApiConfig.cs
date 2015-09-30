using NavigationEdgeApi.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ValueProviders;

namespace NavigationEdgeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
			config.MessageHandlers.Add(new MessageHandler());
			config.Services.Replace(typeof(IHttpControllerSelector), new ControllerSelector());
			config.Services.Insert(typeof(ValueProviderFactory), 0, new DataValueProviderFactory());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{*url}"
            );
        }
    }
}
