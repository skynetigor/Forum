using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Core.DAL.Entities
{
    [Table("LOGTABLE")]
    public class Log:BaseEntity
    {
        public string Time { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}
