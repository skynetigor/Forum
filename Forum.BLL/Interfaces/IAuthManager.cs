using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface IAuthManager : IDisposable
    {
        IEnumerable<UserDTO> GetUsers();
        OperationDetails Create(UserDTO userDto, string password, string url);
        ClaimsIdentity Authenticate(string login, string password);
        IdentityResult ChangePassword(UserDTO user, string currentPassword, string newPassword);
        ClaimsIdentity ConfirmEmail(int token, string email);
    }
}
