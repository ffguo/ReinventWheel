using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public abstract class ControllerBase : IController
    {
        protected HttpRequest Request { get; set; } = HttpContext.Current.Request;
        protected HttpResponse Response { get; set; } = HttpContext.Current.Response;

        public virtual void Execute(RouteData routeData)
        {
            Response.Write(routeData.RouteValue["action"]);
        }
    }
}
