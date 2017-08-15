using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Forum.BLL.Services;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Identity;
using Forum.Core.DAL.Interfaces;
using Forum.NewBLL.Services;
using Forum.NewBLL.Services.CategoriesService;
using Forum.NewDAL.Context;
using Forum.NewDAL.Identity;
using Forum.NewDAL.Repository;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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
            container.Register(Component.For<DbContext>().ImplementedBy<AppContext>()/*.LifestylePerWebRequest()*/);

            container.Register(Component.For<IUserStore<AppUser, int>>().ImplementedBy<UserStoreIntPk>().LifestylePerWebRequest());
            container.Register(Component.For<IRoleStore<ApplicationRole, int>>().ImplementedBy<RoleStoreIntPk>().LifestylePerWebRequest());


            container.Register(Component.For<IIdentityProvider>().ImplementedBy<ForumIdentityProvider>().LifestylePerWebRequest());
            container.Register(Component.For<IAuthManager>().ImplementedBy<AuthManager>().LifestylePerWebRequest());
            container.Register(Component.For<IConfirmedEmailSender>().ImplementedBy<ConfirmedEmailService>().LifestylePerWebRequest());
            container.Register(Component.For<IAdminService>().ImplementedBy<AdminService>().LifestylePerWebRequest());

            container.Register(Component.For<IGenericRepository<Topic>>().ImplementedBy<ForumRepository<Topic>>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Comment>>().ImplementedBy<ForumRepository<Comment>>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Category>>().ImplementedBy<ForumRepository<Category>>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<SubCategory>>().ImplementedBy<ForumRepository<SubCategory>>().LifestylePerWebRequest());
            container.Register(Component.For<IGenericRepository<Block>>().ImplementedBy<ForumRepository<Block>>().LifestylePerWebRequest());

            container.Register(Component.For<IContentService<Topic>>().ImplementedBy<TopicsService>().LifestylePerWebRequest());
            container.Register(Component.For<IContentService<Comment>>().ImplementedBy<CommentService>().LifestylePerWebRequest());
            container.Register(Component.For<IContentService<Category>>().ImplementedBy<MainCategoriesService>().LifestylePerWebRequest());
            container.Register(Component.For<IContentService<SubCategory>>().ImplementedBy<SubCategoryService>().LifestylePerWebRequest());
            container.Register(Component.For<IBlockService>().ImplementedBy<BlockService>().LifestylePerWebRequest());

            var controllers = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(Controller))).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}