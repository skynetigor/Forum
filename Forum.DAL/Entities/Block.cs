using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Entities
{
    public class Block
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public bool IsCommentBlock { get; set; }
        public bool IsAccesBlock { get; set; }
        public bool IsTopicBlock { get; set; }
    }
}
