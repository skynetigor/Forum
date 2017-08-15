using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Identity;

namespace Forum.Core.BLL.Interfaces
{
    public interface IAdminService
    {
        OperationDetails Block(Block blockinfo);
    }
}