using Forum.DAL.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities.Topics
{
    public class TopicDTO:BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
