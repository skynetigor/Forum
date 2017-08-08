using Forum.DAL.Entities;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Entities.Identity.IntPk;
using Forum.DAL.Entities.Topics;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<TopicDTO> Topics { get; set; }
    }
}
