using Forum.BLL.Interfaces;
using System;
using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.DAL.Interfaces;
using Forum.DAL.Entities.Categories;

namespace Forum.BLL.Services
{
    public class CategoryEditorService : ICategoryEditorService
    {
        public CategoryEditorService(IGenericRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        IGenericRepository<Category> categoryRepository;
        public void BlockCategory(UserDTO user, AbstractCategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public void CreateCategory(UserDTO user, AbstractCategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public void RemoveCategory(UserDTO user, AbstractCategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(UserDTO user, AbstractCategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
