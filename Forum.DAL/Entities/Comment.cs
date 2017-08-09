using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Comment: BaseEntity
    {
        public string Message { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
