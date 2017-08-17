using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using System.Collections.Generic;

namespace Forum.BLL.Interfaces
{
    public interface IAdminService
    {
        OperationDetails Block(BlockDTO blockinfo);
    }
}