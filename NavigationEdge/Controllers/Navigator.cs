using EdgeJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NavigationEdge.Controllers
{
	public class Navigator
	{
		private static Func<object, Task<object>> getState = Edge.Func(@"
			var Navigation = require('navigation');
			var StateInfo = require('../../../node/StateInfo');
				
			StateInfo.register();
			return function (data, callback) {
				Navigation.StateController.navigateLink(data);
				callback(null, Navigation.StateContext.state.key);
			}
		");

		public static Task<object> GetState(string url) {
			return getState(url);
		}
	}
}