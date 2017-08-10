using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;

namespace Forum.BLL.Interfaces
{
    public interface IAdminService
    {
        OperationDetails BlockUser(UserDTO admin, UserDTO user, string message);
        OperationDetails UnBlockUser(UserDTO admin, UserDTO user, string message);
    }
}