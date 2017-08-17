using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Content;
using System.Collections.Generic;

namespace Forum.Core.BLL.Interfaces
{
    public interface IBlockService
    {
        IEnumerable<BlockType> GetUserStatusByUserId(int id);
    }
}