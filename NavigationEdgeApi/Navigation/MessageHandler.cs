using EdgeJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

		private Func<object, Task<object>> render = Edge.Func(@"
			var React = require('react');
			var Navigation = require('navigation');
			var StateInfo = require('../../../node/StateInfo');
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data.url);
				var component = React.createElement(Navigation.StateContext.state.component, data.props);
				callback(null, React.renderToString(component));
			}
		");

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			dynamic navigationContext = await context(request.RequestUri.PathAndQuery);
			request.Properties["controller"] = navigationContext.controller;
			request.Properties["data"] = navigationContext.data;
			var response = await base.SendAsync(request, cancellationToken);
			if (request.Content.Headers.ContentType == null)
			{
				var content = (string)await render(new { url = request.RequestUri.PathAndQuery, props = ((ObjectContent)response.Content).Value });
				response.Content = new StringContent(content);
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
			}
			return response;
		}
	}
}