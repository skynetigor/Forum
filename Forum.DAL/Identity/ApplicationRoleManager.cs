using Forum.DAL.Entities;
using Forum.DAL.Entities.Identity.IntPk;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(RoleStoreIntPk store): base(store)
        {
            //if (this.Roles.Count() == 0)
            //{
            //    ApplicationRole[] roles = {
            //        new ApplicationRole {Name = "admin" },
            //        new ApplicationRole {Name = "moderator" },
            //        new ApplicationRole {Name = "user" }
            //    };
            //    foreach (ApplicationRole role in roles)
            //    {
            //        this.Create(role);
            //    }
            //}
        }
    }
}
