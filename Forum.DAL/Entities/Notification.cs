using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Notification:BaseEntity
    {
        public virtual ApplicationUser User { get; set; }
        public string Message { get; set; }
    }
}
