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

namespace Forum.WEB.Util
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // регистрируем компоненты приложения
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            //container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            // регистрируем каждый контроллер по отдельности
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}