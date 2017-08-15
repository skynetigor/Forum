using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Forum.NewBLL.Services.CategoriesService
{
    public abstract class AbstractCategoryService<TCategory> : AbstractContentService<TCategory> where TCategory : class
    {

        protected IIdentityProvider identity;
        protected IGenericRepository<Category> categoryRepository;
        public AbstractCategoryService(IGenericRepository<Category> categoryRepository, IIdentityProvider identity)
        {
            this.identity = identity;
            this.categoryRepository = categoryRepository;
        }
    }
}
