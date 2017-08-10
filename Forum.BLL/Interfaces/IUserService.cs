using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDTO> GetUsers();
        OperationDetails Create(UserDTO userDto, string password, string url);
        ClaimsIdentity Authenticate(string login, string password);
        ClaimsIdentity ConfirmEmail(int token, string email);
    }
}
