using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Entities.Identity.IntPk;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Forum.NewDAL.Identity
{
    public class UserStoreIntPk : UserStore<AppUser, ApplicationRole, int,
             UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public UserStoreIntPk(DbContext context) : base(context)
        {
        }
    }
}
