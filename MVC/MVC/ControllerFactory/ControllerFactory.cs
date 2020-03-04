using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class ControllerFactory : IControllerFactory
    {
        public IController CreateController(RouteData routeData, string controllerName)
        {
            if (routeData == null)
            {
                throw new ArgumentNullException("routeData");
            }

            if (string.IsNullOrEmpty(controllerName))
            {
                throw new ArgumentException("controllerName");
            }

            var controllerType = GetControllerType(routeData, controllerName);
            if (controllerType == null)
                return null;
            return GetControllerInstance(controllerType);
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        private Type GetControllerType(RouteData routeData, string controllerName)
        {
            Type type = null;
            routeData.RouteValue.TryGetValue("namespaces", out string routeNamespaces);
            routeData.RouteValue.TryGetValue("assembly", out string routeAssembly);
            var fullControllerName = controllerName + "Controller";
            if (string.IsNullOrEmpty(routeNamespaces) || string.IsNullOrEmpty(routeAssembly))
            {
                var assembly = Assembly.GetExecutingAssembly();
                type = assembly.GetTypes().First(a => a.IsAssignableFrom(typeof(Controller)) && a.Name == fullControllerName);
            }
            else
            {
                type = Assembly.Load(routeAssembly).GetType(routeNamespaces.ToString() + "." + fullControllerName, false, true);
            }
            return type;
        }

        private IController GetControllerInstance(Type controllerType)
        {
            var controller = Activator.CreateInstance(controllerType) as IController;
            if (controller == null)
                throw new Exception("Can't create controller");
            return controller;
        }
    }
}
