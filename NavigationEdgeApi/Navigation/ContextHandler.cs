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
			var StateInfo = require('../../node/StateInfo');

			// Registers the Navigation router configuration.
			StateInfo.register();
			return function (data, callback) {
				// Sets the Navigation router context from the current Url.
				Navigation.StateController.navigateLink(data);
				// Returns the State key and Route data from the Navigation router context.
				callback(null, { 
					key: Navigation.StateContext.state.key,
					data: Navigation.StateContext.data 
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