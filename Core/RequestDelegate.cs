using Core.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public delegate Task RequestDelegate(HttpContext context);
}
