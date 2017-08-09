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
        public virtual Category Category { get; set; }
        public virtual ICollection<TopicDTO> Topics { get; set; }
    }
}
