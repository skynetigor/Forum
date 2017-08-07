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
        OperationDetails Create(UserDTO userDto);
        ClaimsIdentity Authenticate(UserDTO userDto);
    }
}
