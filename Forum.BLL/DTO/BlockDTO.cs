using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.DTO
{
    public class BlockDTO
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public bool IsComment { get; set; }
        public bool IsAccess { get; set; }
        public bool IsTopic { get; set; }
    }
}
