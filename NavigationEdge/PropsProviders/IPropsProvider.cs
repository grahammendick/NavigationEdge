﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationEdge.PropsProvider
{
	interface IPropsProvider
	{
		Task<IDictionary<string, object>> GetPropsAsync(dynamic data);
	}
}