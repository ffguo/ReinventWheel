using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class MvcHandler : IHttpHandler
    {
        private readonly RouteData _routeData;
        private readonly HttpContextBase _context;

        public MvcHandler(RouteData routeData, HttpContextBase context)
        {
            this._routeData = routeData;
            this._context = context;
        }

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var controllerName = _routeData.RouteValue["controller"];
            IControllerFactory controllerFactory = new ControllerFactory();
            IController controller = controllerFactory.CreateController(_routeData, controllerName);
            if (controller == null)
                return;
            controller.Execute(_routeData);
        }
    }
}
