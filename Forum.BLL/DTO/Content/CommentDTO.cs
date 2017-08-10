using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.DTO.Content
{
    public class CommentDTO:BaseEntityDTO
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public virtual string UserName { get; set; }
        public int TopicId { get; set; }
        public int TopicName { get; set; }
    }
}
