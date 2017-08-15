using Forum.Core.DAL.Entities.Identity;
using System.Collections.Generic;

namespace Forum.Core.DAL.Entities.Content.Categories
{
    public class Category : AbstractCategory
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        public virtual AppUser Moderator { get; set; }
    }
}
