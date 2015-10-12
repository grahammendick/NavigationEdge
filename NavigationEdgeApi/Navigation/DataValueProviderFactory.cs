using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace NavigationEdgeApi.Navigation
{
	public class DataValueProviderFactory : ValueProviderFactory
	{
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			// Uses the current Route data to create a Value Provider.
			return new DataValueProvider((dynamic) actionContext.Request.Properties["data"]);
		}
	}
}