using Forum.BLL.DTO.Content.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.DTO.Content
{
    public class TopicDTO
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
        public SubCategoryDTO SubCategory { get; set; }
    }
}
