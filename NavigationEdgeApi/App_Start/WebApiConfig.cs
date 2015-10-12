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
            // Registers the Message Handlers that call into Node using Edge.js.
			// They return the Navigation router context and render Components as HTML.
			config.MessageHandlers.Add(new ContextHandler());
			config.MessageHandlers.Add(new RenderHandler());
			// Replaces the default Controller lookup mechanism.
			// Uses context set by the ContextHandler to perform the lookup.
			config.Services.Replace(typeof(IHttpControllerSelector), new ControllerSelector());
			// Registers a Navigation Route data Value Provider.
			// Uses context set by the ContextHandler to source its data.
			config.Services.Add(typeof(ValueProviderFactory), new DataValueProviderFactory());

			// Registers just one catch-all route.
			// The Navigation router configuration supplies the Web Api routes.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{*url}"
            );
        }
    }
}
