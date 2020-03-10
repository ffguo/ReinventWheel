using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MVC;

namespace MVC.Demo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var defaultPath = new Dictionary<string, string>();
            defaultPath.Add("namespaces", "MVC.Demo.Controllers");
            defaultPath.Add("assembly", "MVC.Demo");
            defaultPath.Add("controller", "Home");
            defaultPath.Add("action", "Index");
            defaultPath.Add("id", "");
            RouteTable.Routes.Add("Test", new Route("controller/action/id", defaultPath, new MvcRouteHandler()));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}