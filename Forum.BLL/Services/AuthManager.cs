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
        private IUnitOfWork database;
        private IConfirmedEmailSender emailSender = new ConfirmedEmailService();

        public AuthManager(IUnitOfWork database, IConfirmedEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.database = database;
            FirstInitialize();
        }

        public OperationDetails Create(UserDTO userDto, string password, string confirmeUrl)
        {
            var user = database.UserManager.FindByEmail(userDto.Email);

            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = database.UserManager.Create(user, password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                // добавляем роль
                if (userDto.Role != "admin")
                    userDto.Role = "user";
                database.UserManager.AddToRole(user.Id, userDto.Role);
                // создаем профиль клиента
                database.Save();
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
            var user = database.UserManager.FindById(token);
            if (user != null)
            {
                if (user.Email == email)
                {
                    user.EmailConfirmed = true;
                    database.UserManager.Update(user);
                    return database.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
            return null;
        }

        public ClaimsIdentity Authenticate(string login, string password)
        {
            var user = database.UserManager.Find(login, password);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    throw new Exception(EMAIL_NOT_CONFIRM_ERROR);
                }
                return database.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return null;
        }


        private void FirstInitialize()
        {
            int i = database.UserManager.Users.Count();
            int b = database.RoleManager.Roles.Count();
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
                    database.RoleManager.Create(role);
                }
                var user = new UserDTO
                {
                    Email = "admin",
                    Role = "admin",
                    UserName = "Administrator",
                };
                var op = Create(user, "111111", null);
                var u = database.UserManager.Find(user.Email, "111111");
                u.EmailConfirmed = true;
                database.UserManager.Update(u);
            }
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
