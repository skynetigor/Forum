using Forum.DAL.Entities.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities.Categories
{
    public class SubCategory:AbstractCategory
    {
        public SubCategory()
        {
            Topics = new List<TopicDTO>();
        }
        public Category Category { get; set; }
        public ICollection<TopicDTO> Topics { get; set; }
        public ICollection<ApplicationUser> Moderators { get; set; }
    }
}
