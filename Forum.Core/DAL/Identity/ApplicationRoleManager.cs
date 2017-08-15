using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace Forum.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole,int> store): base(store)
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
