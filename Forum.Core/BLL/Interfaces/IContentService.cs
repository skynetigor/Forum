using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Identity;
using System.Collections.Generic;

namespace Forum.Core.BLL.Interfaces
{
    public interface IContentService<TContent>
    {
        IEnumerable<TContent> Get();

        TContent FindById(int id);

        OperationDetails Create(AppUser user, TContent content);

        OperationDetails Update(AppUser user, TContent content);

        OperationDetails Delete(AppUser user, TContent content);
    }
}
