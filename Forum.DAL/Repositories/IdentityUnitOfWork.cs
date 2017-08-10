using Forum.DAL.EF;
using Forum.DAL.Entities;
using Forum.DAL.Entities.Identity.IntPk;
using Forum.DAL.Identity;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public IdentityUnitOfWork(ApplicationContext context)
        {
            this.context = context;
            userManager = new ApplicationUserManager(new UserStoreIntPk(context));
            roleManager = new ApplicationRoleManager(new RoleStoreIntPk(context));
        }

        public UserManager<ApplicationUser, int> UserManager
        {
            get { return userManager; }
        }

        public RoleManager<ApplicationRole, int> RoleManager
        {
            get { return roleManager; }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
