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

		public static Task<object> GetStateContext(string url) {
			return getStateContext(url);
		}
	}
}