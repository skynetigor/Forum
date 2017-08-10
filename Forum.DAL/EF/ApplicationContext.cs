using Forum.DAL.Entities;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Entities.Identity.IntPk;
using Forum.DAL.Entities.Topics;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Forum.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public ApplicationContext() : base("ApplicationContext")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notification { get; set; }
    }
}
