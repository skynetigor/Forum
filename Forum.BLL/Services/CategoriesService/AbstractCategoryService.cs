using Forum.BLL.DTO.Content.Category;
using System.Collections.Generic;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using Forum.BLL.DTO.Content;
using Forum.BLL.DTO;
using System.Linq;

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
                var topics = new List<TopicDTO>();
                var sub = GetSubCategory(subcat);
                subCatList.Add(sub);
            }

            return subCatList;
        }

        protected SubCategoryDTO GetSubCategory(SubCategory subcategory)
        {
            var sub = new SubCategoryDTO
            {
                Name = subcategory.Name,
                Id = subcategory.Id,
                Title = subcategory.Title,
                CategoryId = subcategory.Category.Id,
                CategoryName = subcategory.Category.Name,
                TopicsCount = subcategory.Topics.Count,
                AnswersCount = subcategory.Topics.Sum(t => t.Comments.Count)
            };
            return sub;
        }
    }
}
