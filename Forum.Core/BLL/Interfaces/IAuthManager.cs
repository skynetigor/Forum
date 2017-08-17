using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Model;
using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Forum.Core.BLL.Interfaces
{
    public interface IAuthManager : IDisposable
    {
        AppUser FindById(int Id);
        IEnumerable<AppUser> GetUsers();
        OperationDetails CreateAccount(RegistrationModel user, string confirmeUrl);
        ClaimsIdentity Authenticate(string login, string password);
        OperationDetails ChangeAccountSettings(int userId, string currentPassword, RegistrationModel model);
        ClaimsIdentity ConfirmEmail(int token, string email);
    }
}
