using Forum.DAL.EF;
using Forum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.FirstInitializer
{
    class MyContextInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = "admin",
            };
            db.SaveChanges();
        }
    }
}
