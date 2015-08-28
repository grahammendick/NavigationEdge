using System.Collections.Generic;

namespace NavigationEdge.Controllers
{
	interface IPropsProvider
	{
		IDictionary<string, object> GetProps(dynamic data);
	}
}
