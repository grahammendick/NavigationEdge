using EdgeJs;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace NavigationEdge.Controllers
{
	public class React
	{
		private static Func<object, Task<object>> render = Edge.Func(@"
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

		public static Task<object> Render(string url, object props)
		{
			dynamic data = new ExpandoObject();
			data.url = url;
			data.props = props;
			return render(data);
		}
	}
}