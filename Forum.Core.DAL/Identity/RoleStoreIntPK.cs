using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Entities.Identity.IntPk;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Forum.NewDAL.Identity
{
    public class RoleStoreIntPk : RoleStore<ApplicationRole, int, UserRoleIntPk>
    {
        public RoleStoreIntPk(DbContext context) : base(context)
        {
            
        }
    }
}
