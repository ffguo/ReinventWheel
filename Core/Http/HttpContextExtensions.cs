using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http
{
    public static class HttpContextExtensions
    {
        public static async Task WriteAsync(this HttpResponse response, string content)
        {
            if(string.IsNullOrEmpty(response.Headers.Get("content-type")))
            {
                response.Headers.Add("content-type", "text/html;charset=UTF-8");
            }
            var data = Encoding.UTF8.GetBytes(content);
            await response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}
