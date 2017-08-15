using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;

namespace Forum.Core.DAL.Interfaces
{
    public interface IIdentityProvider : IDisposable
    {
        UserManager<AppUser, int> UserManager { get; }
        RoleManager<ApplicationRole, int> RoleManager { get; }
    }
}
