using System.Web.Mvc;

namespace Forum.IoC.Interfaces
{
    public interface IDependencyInstaller:IDependencyResolver
    {
        DefaultControllerFactory GetControllerFactory();
        T Resolve<T>();
    }
}