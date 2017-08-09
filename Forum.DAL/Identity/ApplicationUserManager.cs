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
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(UserStoreIntPk store) : base(store)
        {
            //if (this.Users.Count() == 0)
            //{
            //    ApplicationUser user = new ApplicationUser
            //    {
            //        Email = "admin",
            //        UserName = "admin",
            //        EmailConfirmed = true
            //    };
            //    this.Create(user, "111111");
            //    this.AddToRole(user.Id, "admin");
            //}
        }
    }
}
