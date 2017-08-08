using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities.Categories
{
    public class AbstractCategory:BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
