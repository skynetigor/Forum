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
                CategoryDTO category = new CategoryDTO
                {
                    Id = subcat.Id,
                    Title = subcat.Title,
                    SubCategories = ExtractSubCategories(subcat.Category)
                };
                SubCategoryDTO sub = new SubCategoryDTO
                {
                    Name = subcat.Name,
                    Id = subcat.Id,
                    Title = subcat.Title,
                    Category = category,
                };
                subCatList.Add(sub);
            }
            return subCatList;
        }

        public override SubCategoryDTO FindById(int id)
        {
            SubCategory subcat = subCategoryRepository.FindById(id);
            CategoryDTO category = new CategoryDTO
            {
                Name = subcat.Name,
                Title = subcat.Title,
                SubCategories = ExtractSubCategories(subcat.Category)
            };
            SubCategoryDTO subcategory = new SubCategoryDTO
            {
                Id = subcat.Id,
                Name = subcat.Name,
                Category = category
            };
            return subcategory;
        }

        protected override OperationDetails CreateContent(UserDTO user, SubCategoryDTO category)
        {

            Category cat = categoryRepository.FindById(category.Category.Id);
            if (identity.UserManager.IsInRole(user.Id, "admin") || cat.Moderator.Id == user.Id)
            {
                SubCategory subCat = new SubCategory
                {
                    Name = category.Name,
                    Title = category.Title,
                };
                cat.SubCategories.Add(subCat);
                categoryRepository.Update(cat);
                return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, cat.Name));
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails UpdateContent(UserDTO user, SubCategoryDTO category)
        {

            SubCategory subCat = subCategoryRepository.FindById(category.Id);
            if (identity.UserManager.IsInRole(user.Id, "admin") || subCat.Category.Moderator.Id == user.Id)
            {
                var newCategory = categoryRepository.FindById(category.Category.Id);
                subCat.Name = category.Name;
                subCat.Title = category.Title;
                subCat.Category = newCategory;
                subCategoryRepository.Update(subCat);
                return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, subCat.Category.Name));
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails DeleteContent(UserDTO user, SubCategoryDTO category)
        {

            SubCategory cat = subCategoryRepository.FindById(category.Id);
            if (identity.UserManager.IsInRole(user.Id, "admin") || cat.Category.Moderator.Id == user.Id)
            {
                subCategoryRepository.Remove(cat);
                return new OperationDetails(true, string.Format(SUBCATEGORY_DELETE_SUCCESS, cat.Name));
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }
    }
}
