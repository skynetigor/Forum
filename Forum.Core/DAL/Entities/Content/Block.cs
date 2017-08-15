using Forum.Core.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Core.DAL.Entities.Content
{
    public class Block
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual AppUser User { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public bool IsComment { get; set; }
        public bool IsAccess { get; set; }
        public bool IsTopic { get; set; }
    }
}
