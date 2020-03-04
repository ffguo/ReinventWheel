using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public interface IRouteHandler
    {
        IHttpHandler GetHttpHandler(RouteData routeData, HttpContextBase context);
    }
}
