using Forum.DAL.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities.Identity.IntPk
{
    
        public class UserRoleIntPk : IdentityUserRole<int>
        {
        }

        public class UserClaimIntPk : IdentityUserClaim<int>
        {
        }

        public class UserLoginIntPk : IdentityUserLogin<int>
        {
        }

        public class RoleIntPk : IdentityRole<int, UserRoleIntPk>
        {
            public RoleIntPk() { }
            public RoleIntPk(string name) { Name = name; }
        }

        public class UserStoreIntPk : UserStore<ApplicationUser, ApplicationRole, int,
            UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
        {
            public UserStoreIntPk(ApplicationContext context)
                : base(context)
            {
            }
        }

        public class RoleStoreIntPk : RoleStore<ApplicationRole, int, UserRoleIntPk>
        {
            public RoleStoreIntPk(ApplicationContext context)
                : base(context)
            {
            }
        }
}
