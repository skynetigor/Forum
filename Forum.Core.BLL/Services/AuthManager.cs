using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Mail;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Interfaces;
using Forum.Core.DAL.Entities.Identity;
using Forum.Core.BLL.Infrastructure;

namespace Forum.NewBLL.Services
{
    public class AuthManager : IAuthManager
    {
        const string SMTP_SERVER = "smtp.gmail.com";
        const string SENDER_EMAIL = "smtp.forum.panasiuk@gmail.com";
        const string PASSWORD = "Bujhm17021995";
        const string DISPLAY_NAME_MAIL = "Web Registration";
        const string MAIL_SUBJECT = "Email confirmation";
        const string MAIL_BODY = "Для завершения регистрации перейдите по ссылке:<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>";
        const string EMAIL_NOT_CONFIRM_ERROR = "Ваш эмейл не подтвежден! Зайдите на свою почту и подтвердите.";

        private IIdentityProvider identity;
        private IConfirmedEmailSender emailSender;
        private IBlockService blockService;



        public AuthManager(IIdentityProvider database, IConfirmedEmailSender emailSender, IBlockService blockService)
        {
            this.emailSender = emailSender;
            this.identity = database;
            this.blockService = blockService;
            FirstInitialize();
        }

        public IEnumerable<AppUser> GetUsers()
        {
            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in identity.UserManager.Users)
            {
                if(!identity.UserManager.IsInRole(user.Id, "admin"))
                {
                    users.Add(user);
                }
            }
            return users;
        }

        public OperationDetails Create(AppUser user, string password, string confirmeUrl)
        {
            var appuser = identity.UserManager.FindByEmail(user.Email);

            if (appuser == null)
            {
                var result = identity.UserManager.Create(user, password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                identity.UserManager.AddToRole(user.Id, "user");
                if (!string.IsNullOrEmpty(confirmeUrl))
                    emailSender.SendConfirmationMessage(GenerateMessage(user, confirmeUrl), PASSWORD);
                return new OperationDetails(true, "Регистрация успешно пройдена");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует");
            }
        }

        public ClaimsIdentity Authenticate(string login, string password)
        {
            var user = identity.UserManager.Find(login, password);
            if (user != null)
            {
                var block = blockService.GetUserStatusByUserId(user.Id);
                if (block != null)
                {
                    if (block.BlockType.Contains(BlockType.Access))
                    {
                        throw new Exception(block.Message);
                    }
                }
                if (!user.EmailConfirmed)
                {
                    throw new Exception(EMAIL_NOT_CONFIRM_ERROR);
                }
                return identity.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return null;
        }

        public IdentityResult ChangePassword(AppUser user, string currentPassword, string newPassword)
        {
            return identity.UserManager.ChangePassword(user.Id, currentPassword, newPassword);
        }

        private MailMessage GenerateMessage(AppUser user, string url)
        {
            var from = new MailAddress(SENDER_EMAIL, DISPLAY_NAME_MAIL);
            var to = new MailAddress(user.Email);
            var message = new MailMessage(from, to);
            message.Subject = MAIL_SUBJECT;
            var paramUrl = string.Format(url, user.Id, user.Email);
            message.Body = string.Format(MAIL_BODY, paramUrl);
            message.IsBodyHtml = true;
            return message;
        }

        public ClaimsIdentity ConfirmEmail(int token, string email)
        {
            var user = identity.UserManager.FindById(token);
            if (user != null)
            {
                if (user.Email == email)
                {
                    user.EmailConfirmed = true;
                    identity.UserManager.Update(user);
                    return identity.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
            return null;
        }

        private void FirstInitialize()
        {
            int i = identity.UserManager.Users.Count();
            int b = identity.RoleManager.Roles.Count();
            if (b == 0)
            {
                ApplicationRole[] roles = {
                    new ApplicationRole {Name = "admin" },
                    new ApplicationRole {Name = "moderator" },
                    new ApplicationRole {Name = "user" },
                };
                foreach (ApplicationRole role in roles)
                {
                    identity.RoleManager.Create(role);
                }
            }
            if (i == 0)
            {
                var user = new AppUser
                {
                    Email = "admin",
                    UserName = "admin",
                };
                var op = identity.UserManager.Create(user, "111111");
                user.EmailConfirmed = true;
                identity.UserManager.AddToRole(user.Id, "admin");
                identity.UserManager.Update(user);
            }
        }

        public void Dispose()
        {
            identity.Dispose();
        }
    }
}
