using Forum.BLL.DTO.Content.Category;
using System.Collections.Generic;
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
                var modId = 0;
                var modName = "";
                if (category.Moderator != null)
                {
                    modId = category.Moderator.Id;
                    modName = category.Moderator.Email;
                }
                var dto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title,
                    ModeratorId = modId,
                    ModeratorName = modName
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
                var modId = 0;
                var modName = "";
                if(category.Moderator != null)
                {
                    modId = category.Moderator.Id;
                    modName = category.Moderator.Email;
                }
                return new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title,
                    SubCategories = ExtractSubCategories(category),
                    ModeratorId = modId,
                    ModeratorName = modName
                };
            }
            return null;
        }

        protected override OperationDetails CreateContent(UserDTO user, CategoryDTO category)
        {
            var appuser = identity.UserManager.FindById(category.ModeratorId);
            if (appuser != null)
            {
                identity.UserManager.AddToRole(appuser.Id, "moderator");
            }
            var cat = new Category
            {
                Id = category.Id,
                Name = category.Name,
                Title = category.Title,
                Moderator = appuser
            };
            categoryRepository.Create(cat);
            return new OperationDetails(true, string.Format(CATEGORY_CREATE_SUCCESS, cat.Name));
        }

        protected override OperationDetails UpdateContent(UserDTO user, CategoryDTO category)
        {
            var appuser = identity.UserManager.FindById(category.ModeratorId);
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
            cat.Title = category.Title;
            cat.Moderator = appuser;
            categoryRepository.Update(cat);
            return new OperationDetails(true, string.Format(CATEGORY_UPDATE_SUCCESS, cat.Name));
        }

        protected override OperationDetails DeleteContent(UserDTO user, CategoryDTO category)
        {
            var cat = categoryRepository.FindById(category.Id);
            categoryRepository.Remove(cat);
            return new OperationDetails(true, string.Format(CATEGORY_DELETE_SUCCESS, cat.Name));
        }
    }
}
