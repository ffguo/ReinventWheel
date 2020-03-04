using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class UrlRoutingModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.PostResolveRequestCache += App_PostResolveRequestCache;
        }

        void App_PostResolveRequestCache(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var httpContext = new HttpContextWrapper(app.Context);
            PostResolveRequestCache(httpContext);
        }

        public virtual void PostResolveRequestCache(HttpContextBase httpContext)
        {
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            if (routeData == null)
                return;
            var routeHandler = routeData.RouteHandler;
            if (routeHandler == null)
                return;

            var httpHandler = routeHandler.GetHttpHandler(routeData, httpContext);
            if (httpHandler == null)
                return;

            httpContext.RemapHandler(httpHandler);
        }
    }
}
