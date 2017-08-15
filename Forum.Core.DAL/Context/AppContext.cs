using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Entities.Identity.IntPk;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Forum.NewDAL.Context
{
    public class AppContext:IdentityDbContext<AppUser, ApplicationRole, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public AppContext() : base("ApplicationContext")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Block> Block { get; set; }
    }
}
