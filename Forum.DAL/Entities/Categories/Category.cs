using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities.Categories
{
    public class Category : AbstractCategory
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
        ICollection<SubCategory> SubCategories { get; set; }
    }
}
