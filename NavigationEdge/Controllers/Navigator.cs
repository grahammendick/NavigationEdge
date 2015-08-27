using EdgeJs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NavigationEdge.Controllers
{
	public class Navigator
	{
		private static Func<object, Task<object>> getStateContext = Edge.Func(@"
			var Navigation = require('navigation');
			var StateInfo = require('../../../node/StateInfo');
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data);
				callback(null, { 
					state: Navigation.StateContext.state.key,
					data: Navigation.StateContext.data 
				});
			}
		");

		private static Func<object, Task<object>> render = Edge.Func(@"
			var React = require('react');
			var Navigation = require('navigation');
			var StateInfo = require('../../../node/StateInfo');
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data.url);
				var component = React.createElement(NavigationShared.getComponent(), data.props);
				callback(null, React.renderToString(component));
			}
		");

		public static Task<object> GetStateContext(string url)
		{
			return getStateContext(url);
		}

		public static Task<object> Render(string url, dynamic props)
		{
			dynamic data = new ExpandoObject();
			data.url = url;
			data.props = props;
			return render(data);
		}
	}
}