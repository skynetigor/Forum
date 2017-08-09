using System.Collections.Generic;
using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Infrastructure;
using Forum.DAL.Entities.Categories;

namespace Forum.BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();

        CategoryDTO FindCategoryById(int id);

        IEnumerable<SubCategoryDTO> GetSubCategories();

        SubCategoryDTO FindSubCategoryById(int id);

        OperationDetails CreateCategory(UserDTO user, CategoryDTO category);

        OperationDetails CreateSubCategory(UserDTO user, CategoryDTO category, SubCategoryDTO subCategory);

        OperationDetails DeleteCategory(UserDTO user, CategoryDTO category);

        OperationDetails DeleteSubCategory(UserDTO user, SubCategoryDTO subCategory);

        OperationDetails UpdateCategory(UserDTO user, CategoryDTO category);

        OperationDetails UpdateSubCategory(UserDTO user, SubCategoryDTO subCategory);
    }
}