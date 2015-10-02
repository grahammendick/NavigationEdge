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
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data);
				callback(null, { 
					name: Navigation.StateContext.state.name,
					data: Navigation.StateContext.data 
				});
			}
		");

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			dynamic navigationContext = await getContext(request.RequestUri.PathAndQuery);
			request.Properties["name"] = navigationContext.name;
			request.Properties["data"] = navigationContext.data;
			return await base.SendAsync(request, cancellationToken);
		}
	}
}