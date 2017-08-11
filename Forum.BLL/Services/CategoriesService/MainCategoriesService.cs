using Forum.BLL.DTO.Content.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Forum.BLL.Services.CategoriesService
{
    public class MainCategoriesService : AbstractCategoryService<CategoryDTO>
    {
        const string CATEGORY_CREATE_SUCCESS = "Категория '{0}' успешно создана!";
        const string CATEGORY_UPDATE_SUCCESS = "Категория '{0}' успешно изменена!";
        const string CATEGORY_DELETE_SUCCESS = "Категория '{0}' успешно удалена!";

        public MainCategoriesService(IGenericRepository<Category> categoryRepository, IGenericRepository<SubCategory> subCategoryRepository, IUnitOfWork identity) : base(categoryRepository, identity)
        {
        }

        public override IEnumerable<CategoryDTO> Get()
        {
            var catList = new List<CategoryDTO>();
            foreach (var category in categoryRepository.Get())
            {
                var dto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title
                };
                dto.SubCategories = ExtractSubCategories(category);
                catList.Add(dto);
            }
            return catList;
        }

        public override CategoryDTO FindById(int id)
        {
            var category = categoryRepository.FindById(id);
            if (category != null)
            {
                return new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title,
                    SubCategories = ExtractSubCategories(category)
                };
            }
            return null;
        }

        protected override OperationDetails CreateContent(UserDTO user, CategoryDTO category)
        {
            if (identity.UserManager.IsInRole(user.Id, ADMIN_ROLE))
            {
                var cat = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title
                };
                categoryRepository.Create(cat);
                return new OperationDetails(true, string.Format(CATEGORY_CREATE_SUCCESS, cat.Name));
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails UpdateContent(UserDTO user, CategoryDTO category)
        {
            if (identity.UserManager.IsInRole(user.Id, ADMIN_ROLE))
            {
                var cat = categoryRepository.FindById(category.Id);
                cat.Name = category.Name;
                cat.Title = category.Title;
                categoryRepository.Update(cat);
                return new OperationDetails(true, string.Format(CATEGORY_UPDATE_SUCCESS, cat.Name));
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails DeleteContent(UserDTO user, CategoryDTO category)
        {
            if (identity.UserManager.IsInRole(user.Id, ADMIN_ROLE))
            {
                var cat = categoryRepository.FindById(category.Id);
                categoryRepository.Remove(cat);
                return new OperationDetails(true, string.Format(CATEGORY_DELETE_SUCCESS, cat.Name));
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }
    }
}
