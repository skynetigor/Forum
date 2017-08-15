using Forum.Core.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Forum.Core.DAL.Identity
{
    public class ForumIdentityProvider : IIdentityProvider
    {
        private RoleManager<ApplicationRole, int> roleManager;
        private UserManager<AppUser, int> userManager;
        public ForumIdentityProvider(IUserStore<AppUser, int> userstore, IRoleStore<ApplicationRole, int> rolestore)
        {
            this.userManager = new UserManager<AppUser, int>(userstore);
            this.roleManager = new RoleManager<ApplicationRole, int>(rolestore);
        }

        public RoleManager<ApplicationRole, int> RoleManager
        {
            get
            {
                return roleManager;
            }
        }

        public UserManager<AppUser, int> UserManager
        {
            get
            {
                return userManager;
            }
        }

        public void Dispose()
        {
            roleManager.Dispose();
            userManager.Dispose();
        }
    }
}
