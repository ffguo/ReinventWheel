using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class RouteCollection
    {
        public Route Route { get; set; }

        public string Name { get; set; }

        //Global.asax里面配置路由规则和默认路由
        public void Add(string name, Route route)
        {
            Route = route;
            Name = name;
        }

        //通过上下文对象得到当前请求的路由表
        public RouteData GetRouteData(HttpContextBase context)
        {
            var routeData = new RouteData();
            routeData.RouteHandler = Route.RouteHandler;
            routeData.RouteValue = new Dictionary<string, string>();

            foreach (var item in Route.DefaultPath)
            {
                routeData.RouteValue.Add(item.Key, item.Value);
            }

            var path = context.Request.AppRelativeCurrentExecutionFilePath.Substring(2);
            var urlParams = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var templateParams = Route.TemplateUrl.Split('/');
            for (int i = 0; i < templateParams.Length; i++)
            {
                if (urlParams.Length <= i)
                    break;

                if (routeData.RouteValue.Keys.Contains(templateParams[i]))
                    routeData.RouteValue[templateParams[i]] = urlParams[i];
                else
                    routeData.RouteValue.Add(templateParams[i], urlParams[i]);
            }

            return routeData;
        }
    }
}
