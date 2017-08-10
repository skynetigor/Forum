using Forum.BLL.DTO.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface ITopicService:IContentService<TopicDTO>
    {
        IEnumerable<TopicDTO> GetTopicsBySubCategoryId(int id);
    }
}
