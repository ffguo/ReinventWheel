using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC
{
    public abstract class ActionResult
    {
        public abstract void ExecuteResult(ControllerContext controllerContext);
    }
}
