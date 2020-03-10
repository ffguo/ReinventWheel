using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class ViewResult : ActionResult
    {
        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            var routeData = controllerContext.RouteData;
            var filepath = AppDomain.CurrentDomain.BaseDirectory + @"Views\" + routeData.RouteValue["controller"] + "\\" + routeData.RouteValue["action"] + ".html";
            var fileContent = Engine.Razor.RunCompile(File.ReadAllText(filepath), filepath, null, Data);
            HttpResponse response = HttpContext.Current.Response;

            response.ContentType = "text/html";
            response.Write(fileContent);
        }
    }
}
