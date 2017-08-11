using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;

namespace Forum.BLL.Interfaces
{
    public interface IBlockService
    {
        BlockDTO GetUserBlockByUserId(int id);
        BlockDTO GetUserStatusByUserId(int id);
    }
}