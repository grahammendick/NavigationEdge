using EdgeJs;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NavigationEdgeApi.Navigation
{
	public class ContextHandler : DelegatingHandler
	{
		private Func<object, Task<object>> getContext = Edge.Func(@"
			var Navigation = require('navigation');
			var Router = require('../../node/Router');

			// Creates the State Navigator.
			var stateNavigator = Router.createStateNavigator();
			return function (data, callback) {
				// Starts the State Navigator from the current Url.
				stateNavigator.start(data);
				// Returns the State key and Route data from the State Navigator context.
				callback(null, { 
					key: stateNavigator.stateContext.state.key,
					data: stateNavigator.stateContext.data 
				});
			}
		");

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// Calls into Node to get the State key and Route data for the requested Url.
			// The key's needed to locate the Web Api Controller (see ControllerSelector).
			// The data's needed for the Web Api Model Binding (see DataValueProviderFactory).
			dynamic navigationContext = await getContext(request.RequestUri.PathAndQuery);
			request.Properties["key"] = navigationContext.key;
			request.Properties["data"] = navigationContext.data;
			return await base.SendAsync(request, cancellationToken);
		}
	}
}