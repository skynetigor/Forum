using Forum.BLL.DTO.Content.Category;
using System.Collections.Generic;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using Forum.BLL.DTO.Content;
using Forum.BLL.DTO;

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
                foreach (var t in subcat.Topics)
                {
                    var user = new UserDTO
                    {
                        Id = t.User.Id,
                        Name = t.User.UserName,
                        Email = t.User.Email
                    };
                    var topic = new TopicDTO
                    {
                        Id = t.Id,
                        Message = t.Message,
                        Title = t.Title,
                        UserName = user.UserName
                    };
                    topics.Add(topic);
                }
                
                var sub = new SubCategoryDTO
                {
                    Name = subcat.Name,
                    Id = subcat.Id,
                    Title = subcat.Title,
                    Category = dto,
                    Topics = topics
                };
                

                subCatList.Add(sub);
            }

            return subCatList;
        }
    }
}
