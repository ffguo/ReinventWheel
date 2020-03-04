using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Route
    {
        public Route(string url, Dictionary<string, string> defaultPath, IRouteHandler routeHandler)
        {
            TemplateUrl = url;
            DefaultPath = defaultPath;
            RouteHandler = routeHandler;
        }
        public string TemplateUrl { get; private set; }

        public IRouteHandler RouteHandler { get; private set; }

        public Dictionary<string, string> DefaultPath { get; private set; }
    }
}
