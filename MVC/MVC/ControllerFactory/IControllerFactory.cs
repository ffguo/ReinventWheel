using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public interface IControllerFactory
    {
        //创建控制器
        IController CreateController(RouteData routeData, string controllerName);

        //释放控制器
        void ReleaseController(IController controller);
    }
}
