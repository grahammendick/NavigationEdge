using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace NavigationEdgeApi.Navigation
{
	public class DataValueProviderFactory : ValueProviderFactory
	{
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			return new DataValueProvider((dynamic) actionContext.Request.Properties["data"]);
		}
	}
}