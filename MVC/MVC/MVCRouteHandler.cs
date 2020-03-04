using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class MvcRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RouteData routeData, HttpContextBase context)
        {
            return new MvcHandler(routeData, context);
        }
    }
}
