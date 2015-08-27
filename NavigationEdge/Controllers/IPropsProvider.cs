using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationEdge.Controllers
{
	interface IPropsProvider
	{
		void SetProps(Dictionary<string, object> props, dynamic data);
	}
}
