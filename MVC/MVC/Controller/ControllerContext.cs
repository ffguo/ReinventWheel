using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class ControllerContext
    {
        public HttpContext HttpContext { get; private set; }

        public RouteData RouteData { get; private set; }

        public ControllerContext(HttpContext httpContext, RouteData routeData)
        {
            HttpContext = httpContext;
            RouteData = routeData;
        }
    }
}
