using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using System.Collections.Generic;

namespace Forum.Core.DAL.Entities.Content
{
    public class Topic:BaseEntity
    {
        public Topic()
        {
            Comments = new List<Comment>();
        }

        public string Description { get; set; }
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
