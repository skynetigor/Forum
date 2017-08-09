using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Forum.BLL.Services.CategoriesService
{
    public abstract class AbstractCategoryService<TCategory> : AbstractContentService<TCategory> where TCategory : AbstractCategoryDTO
    {
        
        protected IUnitOfWork identity;
        protected IGenericRepository<Category> categoryRepository;
        public AbstractCategoryService(IGenericRepository<Category> categoryRepository, IUnitOfWork identity)
        {
            this.identity = identity;
            this.categoryRepository = categoryRepository;
        }
        protected IEnumerable<SubCategoryDTO> ExtractSubCategories(Category category)
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
