using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;

namespace Forum.BLL.Services
{
    public  class AdminService : IAdminService
    {
        const string ACCESS_ERROR = "Доступ запрещен";
        private IUnitOfWork identity;
        private NotificationService notifyService;
        public AdminService(IUnitOfWork identity, NotificationService notifyService)
        {
            this.identity = identity;
            this.notifyService = notifyService;
        }

        private OperationDetails BlockUnBlockUser(UserDTO admin, UserDTO user, string message, bool IsBlock)
        {
            
            try
            {
                var appadmin = identity.UserManager.Users.First(t => t.Id == user.Id);
                if (identity.UserManager.IsInRole(appadmin.Id, "admin"))
                {
                    var appuser = identity.UserManager.Users.First(t => t.Id == user.Id);
                    appuser.IsBlocked = IsBlock;

                    if (IsBlock)
                    {
                        identity.UserManager.AddToRole(appuser.Id, "blocked");
                    }
                    else
                    {
                        identity.UserManager.RemoveFromRole(appuser.Id, "blocked");
                    }
                    notifyService.Notify(user, message);
                    identity.UserManager.Update(appuser);
                    identity.Save();
                }
                return new OperationDetails(false, ACCESS_ERROR, string.Empty);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, string.Empty);
            }
        }

        public OperationDetails BlockUser(UserDTO admin, UserDTO user, string message)
        {
            return BlockUnBlockUser(admin, user, message, true);
        }

        public OperationDetails UnBlockUser(UserDTO admin, UserDTO user, string message)
        {
            return BlockUnBlockUser(admin, user, message, false);
        }
    }
}
