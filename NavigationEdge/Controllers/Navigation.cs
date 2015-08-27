using EdgeJs;
using System;
using System.Threading.Tasks;

namespace NavigationEdge.Controllers
{
	public class Navigation
	{
		private static Func<object, Task<object>> getContext = Edge.Func(@"
			var Navigation = require('navigation');
			var StateInfo = require('../../../node/StateInfo');
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data);
				callback(null, { 
					propsProvider: Navigation.StateContext.state.propsProvider,
					data: Navigation.StateContext.data 
				});
			}
		");

		public static Task<object> GetContext(string url)
		{
			return getContext(url);
		}
	}
}