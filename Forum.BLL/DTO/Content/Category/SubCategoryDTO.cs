using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.DTO.Content.Category
{
    public class SubCategoryDTO:AbstractCategoryDTO
    {
        public SubCategoryDTO()
        {
            Topics = new List<TopicDTO>();
        }
        public CategoryDTO Category { get; set; }
        public ICollection<TopicDTO> Topics { get; set; }
        public ICollection<UserDTO> Moderators { get; set; }
    }
}
