using System.Collections.Generic;

namespace Forum.Core.DAL.Entities.Content.Categories
{
    public class SubCategory:AbstractCategory
    {
        public SubCategory()
        {
            Topics = new List<Topic>();
        }
        public virtual Category Category { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
