using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface IContentService<TContent>
    {
        IEnumerable<TContent> Get();

        TContent FindById(int id);

        OperationDetails Create(UserDTO user, TContent content);

        OperationDetails Update(UserDTO user, TContent content);

        OperationDetails Delete(UserDTO user, TContent content);
    }
}
