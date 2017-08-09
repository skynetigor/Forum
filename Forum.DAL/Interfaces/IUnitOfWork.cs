using Forum.DAL.Entities;
using Forum.DAL.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<ApplicationUser, int> UserManager { get; }
        IClientManager ClientManager { get; }
        RoleManager<ApplicationRole, int> RoleManager { get; }
        void Save();
    }
}
