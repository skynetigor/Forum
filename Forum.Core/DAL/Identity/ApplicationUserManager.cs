using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;


namespace Forum.DAL.Identity
{
    public class ApplicationUserManager : UserManager<AppUser, int>
    {
        public ApplicationUserManager(IUserStore<AppUser,int> store) : base(store)
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
