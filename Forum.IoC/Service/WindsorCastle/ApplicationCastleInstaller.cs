using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Forum.BLL.Interfaces;
using Forum.BLL.Services;
using Forum.DAL.EF;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using Forum.DAL.Repositories;
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
            container.Register(Component.For<ICategoryService>().ImplementedBy<CategoryService>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Category>>().ImplementedBy<Repository<Category>>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<SubCategory>>().ImplementedBy<Repository<SubCategory>>().LifestylePerWebRequest());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<IdentityUnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For<ApplicationContext>().ImplementedBy<ApplicationContext>().LifestylePerWebRequest());
            container.Register(Component.For<IConfirmedEmailSender>().ImplementedBy<ConfirmedEmailService>().LifestylePerWebRequest());
            //container.Register(Component.For<ApplicationContext>().Instance(new ApplicationContext()).LifestylePerWebRequest());

            var controllers = assembly.GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}