using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Interfaces;
using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Identity;

namespace Forum.NewBLL.Services.CategoriesService
{
    public class SubCategoryService : AbstractCategoryService<SubCategory>
    {
        const string SUBCATEGORY_CREATE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно создана!";
        const string SUBCATEGORY_UPDATE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно изменена!";
        const string SUBCATEGORY_DELETE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно удалена!";

        private IGenericRepository<SubCategory> subCategoryRepository;

        public SubCategoryService(IGenericRepository<Category> categoryRepository, IGenericRepository<SubCategory> subCategoryRepository, IIdentityProvider identity) : base(categoryRepository, identity)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public override IEnumerable<SubCategory> Get()
        {
            return subCategoryRepository.Get();
        }

        public override SubCategory FindById(int id)
        {
            return subCategoryRepository.FindById(id);
        }

        protected override OperationDetails CreateContent(AppUser user, SubCategory category)
        {
            Category cat = categoryRepository.FindById(category.Category.Id);
            category.Category = null;
            cat.SubCategories.Add(category);
            categoryRepository.Update(cat);
            return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, category.Name, cat.Name));
        }

        protected override OperationDetails UpdateContent(AppUser user, SubCategory category)
        {
            var cat = categoryRepository.FindById(category.Category.Id);
            var subCat = subCategoryRepository.FindById(category.Id);
            subCat.Name = category.Name;
            subCat.Description = category.Description;
            subCat.Category = cat;
            subCategoryRepository.Update(subCat);
            return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, subCat.Category.Name));
        }

        protected override OperationDetails DeleteContent(AppUser user, SubCategory category)
        {
            SubCategory cat = subCategoryRepository.FindById(category.Id);
            subCategoryRepository.Remove(cat);
            return new OperationDetails(true, string.Format(SUBCATEGORY_DELETE_SUCCESS, cat.Name));
        }
    }
}
