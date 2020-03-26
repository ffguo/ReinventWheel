using System;
using System.Threading.Tasks;
using Core;
using Core.Http;

namespace Core.Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var server = new HttpListenerServer();
            //await server.StartAsync(Handler);

            await new WebHostBuilder()
                .UseServer(new HttpListenerServer())
                .Configure(app =>
                {
                    app.Use(First)
                       .Use(Second);
                })
                .Build()
                .StartAsync();
        }

        static async Task Handler(HttpContext context)
        {
            await context.Response.WriteAsync(context.Request.Url + " - Hello World!");
        }

        static RequestDelegate First(RequestDelegate next)
        {
            return async context => 
            {
                await context.Response.WriteAsync("第一个中间件 => ");
                await next(context);
            };
        }

        static RequestDelegate Second(RequestDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("第二个中间件 => ");
                await next(context);
            };
        }
    }
}
