using Forum.IoC.Interfaces;
using Forum.IoC.Service;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forum.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IDependencyInstaller dependencyInstaller = new CastleInstaller(Assembly.GetExecutingAssembly());
            ControllerBuilder.Current.SetControllerFactory(dependencyInstaller.GetControllerFactory());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
