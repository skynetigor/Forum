using Forum.Core.DAL.Entities.Identity;

namespace Forum.Core.DAL.Entities.Content
{
    public class Notification:BaseEntity
    {
        //[NotMapped]
        public virtual AppUser User { get; set; }
        public string Message { get; set; }
    }
}
