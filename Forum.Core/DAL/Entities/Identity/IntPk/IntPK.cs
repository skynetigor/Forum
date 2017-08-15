using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum.Core.DAL.Entities.Identity.IntPk
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
}
