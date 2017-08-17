using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    public class BlockViewModel
    {
        public int UserId { get; set; }
        public bool IsComment { get; set; }
        public bool IsAccess { get; set; }
        public bool IsTopic { get; set; }
    }
}