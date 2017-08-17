using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Linq;

namespace Forum.NewBLL.Services
{
    public class AdminService : IAdminService
    {
        const string LOG = "Пользователю '{0}' были изменены права доступа - Комментирование: {1}, Создание топиков:{2}, Полный доступ:{3}";
        private IIdentityProvider identity;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AdminService(IIdentityProvider identity)
        {
            this.identity = identity;
        }


        public OperationDetails Block(AppUser user)
        {
            try
            {
                var appuser = identity.UserManager.Users.First(t => t.Id == user.Id);
                appuser.IsAccessBlocked = user.IsAccessBlocked;
                appuser.IsCommentBlocked = user.IsCommentBlocked;
                appuser.IsTopicBlocked = user.IsTopicBlocked;
                identity.UserManager.Update(appuser);
                Log(appuser);
                return new OperationDetails(true, string.Empty);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message);
            }
        }

        private void Log(AppUser user)
        {
            string message = string.Format(LOG, user.UserName, user.IsCommentBlocked, user.IsTopicBlocked, user.IsAccessBlocked);
            logger.Info(message);
        }
    }
}
