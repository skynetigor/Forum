using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Forum.BLL.Interfaces;
using Forum.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Forum.IoC.Service
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public ApplicationCastleInstaller(Assembly installedAseembly)
        {
            this.assembly = installedAseembly;
        }
        private Assembly assembly;
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            var controllers = assembly.GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}