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
           var method = GetType().GetMethod(routeData.RouteValue["action"]);
            List<object> methodParams = new List<object>();
            foreach (var param in method.GetParameters())
            {
                var value = Request.QueryString[param.Name];
                if(param.ParameterType == typeof(string))
                {
                    methodParams.Add(value);
                }
                else
                {
                    methodParams.Add(Convert.ChangeType(Convert.ToInt32(value), param.ParameterType));
                }
            }
            var result = method.Invoke(this, methodParams.ToArray());

            if(result != null && result is ActionResult)
            {
                var actionResult = result as ActionResult;
                actionResult.ExecuteResult(new ControllerContext(HttpContext.Current, routeData));
            }
        }
    }
}
