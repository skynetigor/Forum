using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Forum.BLL.Interfaces;
using Forum.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Forum.DAL.Repositories;
using System;
using System.Net.Mail;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum.BLL.Services
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
        private IUnitOfWork identity;
        private IConfirmedEmailSender emailSender = new ConfirmedEmailService();

        IBlockService blockService;
        public AuthManager(IUnitOfWork database, IConfirmedEmailSender emailSender, IBlockService blockService)
        {
            this.emailSender = emailSender;
            this.identity = database;
            this.blockService = blockService;
            FirstInitialize();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var userList = new List<UserDTO>();
            foreach (var appuser in identity.UserManager.Users)
            {
                if (!identity.UserManager.IsInRole(appuser.Id, "admin"))
                {
                    var user = new UserDTO
                    {
                        Id = appuser.Id,
                        Name = appuser.UserName,
                        Email = appuser.Email,
                        IsBlocked = appuser.IsBlocked
                    };
                    userList.Add(user);
                }
            }
            return userList;
        }

        public OperationDetails Create(UserDTO userDto, string password, string confirmeUrl)
        {
            var user = identity.UserManager.FindByEmail(userDto.Email);

            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = identity.UserManager.Create(user, password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                // добавляем роль
                if (userDto.Role != "admin")
                    userDto.Role = "user";
                identity.UserManager.AddToRole(user.Id, userDto.Role);
                // создаем профиль клиента
                identity.Save();
                //user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
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

        public IdentityResult ChangePassword(UserDTO user, string currentPassword, string newPassword)
        {
            return identity.UserManager.ChangePassword(user.Id, currentPassword, newPassword);
        }

        private MailMessage GenerateMessage(ApplicationUser user, string url)
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
            if (i == 0 && b == 0)
            {
                ApplicationRole[] roles = {
                    new ApplicationRole {Name = "admin" },
                    new ApplicationRole {Name = "moderator" },
                    new ApplicationRole {Name = "user" },
                    new ApplicationRole {Name = "blocked" }
                };
                foreach (ApplicationRole role in roles)
                {
                    identity.RoleManager.Create(role);
                }
                var user = new UserDTO
                {
                    Email = "admin",
                    Role = "admin",
                    UserName = "Administrator",
                };
                var op = Create(user, "111111", null);
                var u = identity.UserManager.Find(user.Email, "111111");
                u.EmailConfirmed = true;
                identity.UserManager.Update(u);
            }
        }

        public void Dispose()
        {
            identity.Dispose();
        }
    }
}
