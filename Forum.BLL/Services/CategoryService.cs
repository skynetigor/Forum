using Forum.BLL.Interfaces;
using System;
using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.DAL.Interfaces;
using Forum.DAL.Entities.Categories;
using System.Collections.Generic;

namespace Forum.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        IGenericRepository<Category> categoryRepository;

        public void CreateCategory(UserDTO currentUser, int CategoryId)
        {
            
        }

        public void RemoveCategory(UserDTO currentUser, int CategoryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(UserDTO currentUser, int CategoryId)
        {
            throw new NotImplementedException();
        }

        public void BlockCategory(UserDTO currentUser, int CategoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
