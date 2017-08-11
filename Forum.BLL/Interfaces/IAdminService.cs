using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using System.Collections.Generic;

namespace Forum.BLL.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserDTO> GetUsers();
        OperationDetails Block(BlockDTO blockinfo);
    }
}