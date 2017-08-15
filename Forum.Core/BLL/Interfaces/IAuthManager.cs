using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Forum.Core.BLL.Interfaces
{
    public interface IAuthManager : IDisposable
    {
        IEnumerable<AppUser> GetUsers();
        OperationDetails Create(AppUser userDto, string password, string url);
        ClaimsIdentity Authenticate(string login, string password);
        IdentityResult ChangePassword(AppUser user, string currentPassword, string newPassword);
        ClaimsIdentity ConfirmEmail(int token, string email);
    }
}
