using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class RouteData
    {
        public IRouteHandler RouteHandler { get; set; }

        public Dictionary<string, string> RouteValue { get; set; }
    }
}
