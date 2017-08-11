using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Forum.BLL.DTO.Content;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using Forum.BLL.Services;
using Forum.BLL.Services.CategoriesService;
using Forum.DAL.EF;
using Forum.DAL.Entities;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Entities.Topics;
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
            container.Register(Component.For<IAuthManager>().ImplementedBy<AuthManager>().LifestylePerWebRequest());
            container.Register(Component.For<IContentService<CategoryDTO>>().ImplementedBy<MainCategoriesService>().LifestylePerWebRequest());
            container.Register(Component.For<IContentService<SubCategoryDTO>>().ImplementedBy<SubCategoryService>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Category>>().ImplementedBy<Repository<Category>>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<SubCategory>>().ImplementedBy<Repository<SubCategory>>().LifestylePerWebRequest());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<IdentityUnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For<ApplicationContext>().ImplementedBy<ApplicationContext>().LifestylePerWebRequest());
            container.Register(Component.For<IConfirmedEmailSender>().ImplementedBy<ConfirmedEmailService>().LifestylePerWebRequest());
            container.Register(Component.For<ITopicService>().ImplementedBy<TopicsService>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Topic>>().ImplementedBy<Repository<Topic>>().LifestylePerWebRequest());
            container.Register(Component.For<ICommentService>().ImplementedBy<CommentService>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Comment>>().ImplementedBy<Repository<Comment>>().LifestylePerWebRequest());

            container.Register(Component.For<INotificationService>().ImplementedBy<NotificationService>().LifestylePerWebRequest());//var controllers = assembly.GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            container.Register(Component.For<IGenericRepository<Notification>>().ImplementedBy<Repository<Notification>>().LifestylePerWebRequest());
            container.Register(Component.For<IAdminService>().ImplementedBy<AdminService>().LifestylePerWebRequest());
            container.Register(Component.For<IBlockService>().ImplementedBy<BlockService>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Block>>().ImplementedBy<Repository<Block>>().LifestylePerWebRequest());


            var controllers = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(Controller))).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}