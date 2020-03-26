using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Http;
using System.Threading.Tasks;

namespace Core
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private List<Func<RequestDelegate, RequestDelegate>> middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            middlewares.Reverse();

            return httpContext =>
            {
                RequestDelegate next = context =>
                {
                    context.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };

                foreach (var middleware in middlewares)
                {
                    next = middleware(next);
                }

                return next(httpContext);
            };
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            middlewares.Add(middleware);
            return this;
        }
    }
}
