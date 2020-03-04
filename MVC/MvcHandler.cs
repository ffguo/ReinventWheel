using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public class TestMvcHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("当前页面地址：" + context.Request.Url.AbsoluteUri + "    ");
            context.Response.Write("Hello MVC");
        }
    }
}
