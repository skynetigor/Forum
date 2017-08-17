using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Notification:BaseEntity
    {
        //[NotMapped]
        public virtual ApplicationUser User { get; set; }
        public string Message { get; set; }
    }
}
