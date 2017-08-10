using Forum.BLL.DTO;
using Forum.BLL.Interfaces;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork identity;
        private IGenericRepository<Notification> notifyRepository;
        public NotificationService(IUnitOfWork identity, IGenericRepository<Notification> notifyRepository)
        {
            this.identity = identity;
            this.notifyRepository = notifyRepository;
        }

        public IEnumerable<NotificationDTO> GetNotificationsByUserId(int userid)
        {
            var appuser = identity.UserManager.FindById(userid);
            var notifylist = new List<NotificationDTO>();
            foreach(var n in appuser.Notifications)
            {
                var notify = new NotificationDTO
                {
                    Message = n.Message,
                    UserId = n.User.Id
                };
                notifylist.Add(notify);
            }
            return notifylist;
        }

        public void Notify(UserDTO user, string message)
        {
            var appuser = identity.UserManager.Users.First(t=> t.Id == user.Id);
            var notification = new Notification {
                Message = message,
                User = appuser
            };
            appuser.Notifications.Add(notification);
            identity.UserManager.Update(appuser);
            identity.Save();
        }
    }
}
