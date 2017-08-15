using Forum.Core.DAL.Entities.Identity;
using System;

namespace Forum.Core.DAL.Entities.Content
{
    public class Comment: BaseEntity
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
