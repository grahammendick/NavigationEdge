using System.Collections.Generic;

namespace NavigationEdge.PropsProvider
{
	interface IPropsProvider
	{
		IDictionary<string, object> GetProps(dynamic data);
	}
}
