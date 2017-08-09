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

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<TopicDTO> Topics { get; set; }
    }
}
