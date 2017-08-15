using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Content;

namespace Forum.Core.BLL.Interfaces
{
    public interface IBlockService
    {
        Block GetUserBlockByUserId(int id);
        BlockResult GetUserStatusByUserId(int id);
    }
}