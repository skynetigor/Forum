using Forum.DAL.Entities.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Comment: BaseEntity
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
