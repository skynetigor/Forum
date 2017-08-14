using Forum.BLL.DTO.Content.Category;
using System.Collections.Generic;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Forum.BLL.Services.CategoriesService
{
    public class SubCategoryService : AbstractCategoryService<SubCategoryDTO>
    {
        const string SUBCATEGORY_CREATE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно создана!";
        const string SUBCATEGORY_UPDATE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно изменена!";
        const string SUBCATEGORY_DELETE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно удалена!";

        private IGenericRepository<SubCategory> subCategoryRepository;

        public SubCategoryService(IGenericRepository<Category> categoryRepository, IGenericRepository<SubCategory> subCategoryRepository, IUnitOfWork identity) : base(categoryRepository, identity)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public override IEnumerable<SubCategoryDTO> Get()
        {
            List<SubCategoryDTO> subCatList = new List<SubCategoryDTO>();
            foreach (SubCategory subcat in subCategoryRepository.Get())
            {
                SubCategoryDTO sub = this.GetSubCategory(subcat);
                subCatList.Add(sub);
            }
            return subCatList;
        }

        public override SubCategoryDTO FindById(int id)
        {
            SubCategory subcat = subCategoryRepository.FindById(id);
            SubCategoryDTO subcategory = this.GetSubCategory(subcat);
            return subcategory;
        }

        protected override OperationDetails CreateContent(UserDTO user, SubCategoryDTO category)
        {
            Category cat = categoryRepository.FindById(category.CategoryId);
            SubCategory subCat = new SubCategory
            {
                Name = category.Name,
                Title = category.Title,
            };
            cat.SubCategories.Add(subCat);
            categoryRepository.Update(cat);
            return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, cat.Name));
        }

        protected override OperationDetails UpdateContent(UserDTO user, SubCategoryDTO category)
        {
            SubCategory subCat = subCategoryRepository.FindById(category.Id);
            var newCategory = categoryRepository.FindById(category.CategoryId);
            subCat.Name = category.Name;
            subCat.Title = category.Title;
            subCat.Category = newCategory;
            subCategoryRepository.Update(subCat);
            return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, subCat.Category.Name));
        }

        protected override OperationDetails DeleteContent(UserDTO user, SubCategoryDTO category)
        {
            SubCategory cat = subCategoryRepository.FindById(category.Id);
            subCategoryRepository.Remove(cat);
            return new OperationDetails(true, string.Format(SUBCATEGORY_DELETE_SUCCESS, cat.Name));
        }
    }
}
