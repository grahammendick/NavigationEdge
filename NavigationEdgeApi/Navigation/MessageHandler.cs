using EdgeJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NavigationEdgeApi.Navigation
{
	public class MessageHandler : DelegatingHandler
	{
		private Func<object, Task<object>> context = Edge.Func(@"
			var Navigation = require('navigation');
			var StateInfo = require('../../../node/StateInfo');
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data);
				callback(null, { 
					controller: Navigation.StateContext.state.controller,
					data: Navigation.StateContext.data 
				});
			}
		");

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			dynamic navigationContext = await context(request.RequestUri.PathAndQuery);
			request.Properties["controller"] = navigationContext.controller;
			request.Properties["data"] = navigationContext.data;
			return await base.SendAsync(request, cancellationToken);
		}
	}
}