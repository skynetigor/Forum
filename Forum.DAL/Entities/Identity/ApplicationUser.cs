using Forum.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Forum.DAL.Entities.Identity.IntPk;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.DAL.Entities
{
    public class ApplicationUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public ApplicationUser():base()
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
