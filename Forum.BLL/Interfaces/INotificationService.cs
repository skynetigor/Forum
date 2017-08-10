using Forum.BLL.DTO;
using System.Collections.Generic;

namespace Forum.BLL.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<NotificationDTO> GetNotificationsByUserId(int userid);
        void Notify(UserDTO user, string message);
    }
}