using Forum.BLL.DTO.Content;
using Forum.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;

namespace Forum.BLL.Services
{
    public class TopicService : IContentService<TopicDTO>
    {
        public IEnumerable<TopicDTO> Get()
        {
            throw new NotImplementedException();
        }

        public TopicDTO FindById(int id)
        {
            throw new NotImplementedException();
        }

        public OperationDetails Create(UserDTO user, TopicDTO category)
        {
            throw new NotImplementedException();
        }

        public OperationDetails Update(UserDTO user, TopicDTO category)
        {
            throw new NotImplementedException();
        }

        public OperationDetails Delete(UserDTO user, TopicDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
