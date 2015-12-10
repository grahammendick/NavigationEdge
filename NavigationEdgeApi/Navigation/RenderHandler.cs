using EdgeJs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NavigationEdgeApi.Navigation
{
	public class RenderHandler : DelegatingHandler
	{
		private Func<object, Task<object>> render = Edge.Func(@"
			var React = require('react');
			var ReactDOMServer = require('react-dom/server');
			var Navigation = require('navigation');
			var StateInfo = require('../../node/StateInfo');

			// Registers the Navigation router configuration.
			StateInfo.register();
			return function (data, callback) {
				// Sets the Navigation router context from the current Url.
				Navigation.StateController.navigateLink(data.url);
				// Creates the State's Component from the props.
				var component = React.createElement(Navigation.StateContext.state.component, data.props);
				// Returns the rendered HTML.
				callback(null, ReactDOMServer.renderToString(component));
			}
		");

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);
			var contentType = request.Content.Headers.ContentType;
			response.Headers.Vary.Add("Content-Type");
			// Returns HTML if it's not an Ajax request.
			if (contentType == null || contentType.MediaType != "application/json")
			{
				// Converst the JSON response into props.
				var props = new Dictionary<string, object> { { (string)request.Properties["key"], ((ObjectContent)response.Content).Value } };
				// Calls into Node to get the rendered HTML for the props and requested Url.
				var content = (string)await render(new { url = request.RequestUri.PathAndQuery, props = props });
				// Replaces the JSON response with the HTML.
				// Includes the props in the response so React can render when the page loads.
				response.Content = new StringContent(string.Format(Resource.Template, content, new JavaScriptSerializer().Serialize(props)));
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
			}
			return response;
		}
	}
}