using System.Collections.Generic;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Interfaces;
using Forum.Core.BLL.Infrastructure;
using Microsoft.AspNet.Identity;
using Forum.Core.DAL.Entities.Identity;
using NLog;

namespace Forum.NewBLL.Services.CategoriesService
{
    public class MainCategoriesService : AbstractCategoryService<Category>
    {
        const string CATEGORY_CREATE_SUCCESS = "Категория '{0}' успешно создана!";
        const string CATEGORY_UPDATE_SUCCESS = "Категория '{0}' успешно изменена!";
        const string CATEGORY_DELETE_SUCCESS = "Категория '{0}' успешно удалена!";
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public MainCategoriesService(IGenericRepository<Category> categoryRepository, IGenericRepository<SubCategory> subCategoryRepository, IIdentityProvider identity) : base(categoryRepository, identity)
        {
        }

        public override IEnumerable<Category> Get()
        {
            return this.categoryRepository.Get();
        }

        public override Category FindById(int id)
        {
            return this.categoryRepository.FindById(id);
        }

        protected override OperationDetails CreateContent(AppUser user, Category category)//
        {
            var appuser = identity.UserManager.FindById(category.Moderator.Id);
            if (appuser != null)
            {
                identity.UserManager.AddToRole(appuser.Id, "moderator");
            }
            category.Moderator = appuser;
            categoryRepository.Create(category);
            string message = string.Format(CATEGORY_CREATE_SUCCESS, category.Name);
            logger.Info(message);
            return new OperationDetails(true, string.Format(CATEGORY_CREATE_SUCCESS, category.Name));
        }

        protected override OperationDetails UpdateContent(AppUser user, Category category)//
        {
            var appuser = identity.UserManager.FindById(category.Moderator.Id);
            var cat = categoryRepository.FindById(category.Id);

            var currModerator = cat.Moderator;
            if (currModerator != null)
            {
                identity.UserManager.RemoveFromRole(cat.Moderator.Id, "moderator");
            }
            if (appuser != null)
            {
                identity.UserManager.AddToRole(appuser.Id, "moderator");
            }
            cat.Name = category.Name;
            cat.Description = category.Description;
            cat.Moderator = appuser;
            categoryRepository.Update(cat);
            string message = string.Format(CATEGORY_UPDATE_SUCCESS, category.Name);
            logger.Info(message);
            return new OperationDetails(true, message);
        }

        protected override OperationDetails DeleteContent(AppUser user, Category category)//
        {
            var cat = categoryRepository.FindById(category.Id);
            categoryRepository.Remove(cat);
            return new OperationDetails(true, string.Format(CATEGORY_DELETE_SUCCESS, cat.Name));
        }
    }
}
