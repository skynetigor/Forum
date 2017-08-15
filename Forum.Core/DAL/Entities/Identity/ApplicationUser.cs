using Forum.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Core.DAL.Entities.Identity.IntPk;
using Forum.Core.DAL.Entities.Content;

namespace Forum.Core.DAL.Entities.Identity
{
    public class AppUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public AppUser():base()
        {
            Notifications = new List<Notification>();
        }
        public bool IsBlocked { get; set; }
        public virtual Block Block { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                               DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
