using Forum.BLL.Interfaces;
using System;
using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.DAL.Interfaces;
using Forum.DAL.Entities.Categories;
using System.Collections.Generic;
using Forum.BLL.Infrastructure;
using System.Linq;
using Microsoft.AspNet.Identity;
using Forum.DAL.Entities;
using Forum.DAL.Repositories;
using Forum.DAL.EF;

namespace Forum.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        const string ACCESS_ERROR = "У вас нет прав на данное действие!";
        const string CATEGORY_CREATE_SUCCESS = "Категория '{0}' успешно создана!";
        const string SUBCATEGORY_CREATE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно создана!";
        const string CATEGORY_UPDATE_SUCCESS = "Категория '{0}' успешно изменена!";
        const string SUBCATEGORY_UPDATE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно изменена!";
        const string CATEGORY_DELETE_SUCCESS = "Категория '{0}' успешно удалена!";
        const string SUBCATEGORY_DELETE_SUCCESS = "Подкатегория '{0}' в категории {1} успешно удалена!";

        private IGenericRepository<Category> categoryRepository;
        private IGenericRepository<SubCategory> subCategoryRepository;
        private IUnitOfWork identity;

        public CategoryService(IGenericRepository<Category> categoryRepository, IGenericRepository<SubCategory> subCategoryRepository, IUnitOfWork identity)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.identity = identity;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            List<CategoryDTO> catList = new List<CategoryDTO>();
            foreach(Category category in categoryRepository.Get())
            {
                CategoryDTO dto = new CategoryDTO
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

        public IEnumerable<SubCategoryDTO> GetSubCategories()
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

        public SubCategoryDTO FindSubCategoryById(int id)
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

        public CategoryDTO FindCategoryById(int id)
        {
           Category category = categoryRepository.FindById(id);
           if(category != null)
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

        public OperationDetails CreateCategory(UserDTO user, CategoryDTO category)
        {
            try
            {
                if(identity.UserManager.IsInRole(user.Id, "admin"))
                {
                    Category cat = new Category
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Title = category.Title
                    };
                    categoryRepository.Create(cat);
                    return new OperationDetails(true, string.Format(CATEGORY_CREATE_SUCCESS, cat.Name) , string.Empty);
                }
                return new OperationDetails(false, ACCESS_ERROR, "");
            }
            catch(Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }

        public OperationDetails UpdateCategory(UserDTO user, CategoryDTO category)
        {
            try
            {
                if (identity.UserManager.IsInRole(user.Id, "admin"))
                {
                    Category cat = categoryRepository.FindById(category.Id);
                    cat.Name = category.Name;
                    cat.Title = category.Title;
                    categoryRepository.Update(cat);
                    return new OperationDetails(true, string.Format(CATEGORY_UPDATE_SUCCESS, cat.Name), string.Empty);
                }
                return new OperationDetails(false, ACCESS_ERROR, "");
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }

        public OperationDetails DeleteCategory(UserDTO user, CategoryDTO category)
        {
            try
            {
                if (identity.UserManager.IsInRole(user.Id, "admin"))
                {
                    Category cat = categoryRepository.FindById(category.Id);
                    categoryRepository.Remove(cat);
                    return new OperationDetails(true, string.Format(CATEGORY_DELETE_SUCCESS, cat.Name), string.Empty);
                }
                return new OperationDetails(false, ACCESS_ERROR, "");
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }

        public OperationDetails CreateSubCategory(UserDTO user, CategoryDTO category, SubCategoryDTO subCategory)
        {
            try
            {
                Category cat = categoryRepository.FindById(category.Id);
                if (identity.UserManager.IsInRole(user.Id, "admin") || cat.Moderator.Id == user.Id)
                {
                    SubCategory subCat = new SubCategory
                    {
                        Name = subCategory.Name,
                        Title = subCategory.Title
                    };
                    cat.SubCategories.Add(subCat);
                    categoryRepository.Update(cat);
                    return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, cat.Name), string.Empty);
                }
                return new OperationDetails(false, ACCESS_ERROR, string.Empty);
            }
            catch(Exception e)
            {
                return new OperationDetails(false, e.Message, string.Empty);
            }
        }

        public OperationDetails UpdateSubCategory(UserDTO user, SubCategoryDTO subCategory)
        {
            try
            {
                SubCategory subCat = subCategoryRepository.FindById(subCategory.Id);
                if (identity.UserManager.IsInRole(user.Id, "admin") || subCat.Category.Moderator.Id == user.Id)
                {
                    var newCategory = categoryRepository.FindById(subCategory.Category.Id);
                    subCat.Name = subCategory.Name;
                    subCat.Title = subCategory.Title;
                    subCat.Category = newCategory;
                    subCategoryRepository.Update(subCat);
                    return new OperationDetails(false, string.Format(SUBCATEGORY_CREATE_SUCCESS, subCat.Name, subCat.Category.Name), string.Empty);
                }
                return new OperationDetails(false, ACCESS_ERROR, string.Empty);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, string.Empty);
            }
        }

        public OperationDetails DeleteSubCategory(UserDTO user, SubCategoryDTO subCategory)
        {
            try
            {
                SubCategory cat = subCategoryRepository.FindById(subCategory.Id);
                if (identity.UserManager.IsInRole(user.Id, "admin") || cat.Category.Moderator.Id == user.Id)
                {
                    subCategoryRepository.Remove(cat);
                    return new OperationDetails(true, string.Format(SUBCATEGORY_DELETE_SUCCESS, cat.Name), string.Empty);
                }
                return new OperationDetails(false, ACCESS_ERROR, "");
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }

        private IEnumerable<SubCategoryDTO> ExtractSubCategories(Category category)
        {
            List<SubCategoryDTO> subCatList = new List<SubCategoryDTO>();
            CategoryDTO dto = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Title = category.Title
            };
            foreach (SubCategory subcat in category.SubCategories)
            {
                SubCategoryDTO sub = new SubCategoryDTO
                {
                    Name = subcat.Name,
                    Id = subcat.Id,
                    Title = subcat.Title,
                    Category = dto,
                };
                subCatList.Add(sub);
            }
            return subCatList;
        }
    }
}
