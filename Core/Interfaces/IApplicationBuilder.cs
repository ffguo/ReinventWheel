using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        RequestDelegate Build();
    }
}
