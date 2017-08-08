using Castle.Windsor;
using Forum.IoC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Forum.IoC.Service
{
    public class CastleInstaller : IDependencyInstaller
    {
        public CastleInstaller(Assembly currentAssembly)
        {
            container = new WindsorContainer();
            this.currentAssembly = currentAssembly;
            Install();
        }
        WindsorContainer container;
        Assembly currentAssembly;
        DefaultControllerFactory factory;
        private void Install()
        {
            container.Install(new ApplicationCastleInstaller(currentAssembly));
            factory = new CastleControllerFactory(container);
        }

        public DefaultControllerFactory GetControllerFactory()
        {
            return factory;
        }

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll<object>(serviceType);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
