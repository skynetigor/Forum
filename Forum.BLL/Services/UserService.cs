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
    public class UserService : IUserService
    {
        const string SMTP_SERVER = "smtp.gmail.com";
        const string SENDER_EMAIL = "smtp.forum.panasiuk@gmail.com";
        const string PASSWORD = "Bujhm17021995";
        const string DISPLAY_NAME_MAIL = "Web Registration";
        const string MAIL_SUBJECT = "Email confirmation";
        const string MAIL_BODY = "Для завершения регистрации перейдите по ссылке:<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>";
        const string EMAIL_NOT_CONFIRM_ERROR = "Ваш эмейл не подтвежден! Зайдите на свою почту и подтвердите.";
        private IUnitOfWork database;
        private IConfirmedEmailSender emailSender =new ConfirmedEmailService();

        public UserService(IUnitOfWork database, IConfirmedEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.database = database;
            FirstInitialize();
        }
        
        public OperationDetails Create(UserDTO userDto, string confirmeUrl)
        {
            ApplicationUser user = null;
            
            user = database.UserManager.FindByEmail(userDto.Email);

            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = database.UserManager.Create(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), string.Empty);
                // добавляем роль
                if (userDto.Role != "admin")
                    userDto.Role = "user";
                database.UserManager.AddToRole(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                database.ClientManager.Create(clientProfile);
                database.Save();
                //user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                if (!string.IsNullOrEmpty(confirmeUrl))
                    emailSender.SendConfirmationMessage(GenerateMessage(user, confirmeUrl), PASSWORD);
                return new OperationDetails(true, "Регистрация успешно пройдена", string.Empty);
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        private MailMessage GenerateMessage(ApplicationUser user, string url)
        {
            MailAddress from = new MailAddress(SENDER_EMAIL, DISPLAY_NAME_MAIL);
            MailAddress to = new MailAddress(user.Email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = MAIL_SUBJECT;
            string paramUrl = string.Format(url, user.Id, user.Email);
            message.Body = string.Format(MAIL_BODY, paramUrl);
            message.IsBodyHtml = true;
            return message;
        }

        public ClaimsIdentity ConfirmEmail(int token, string email)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = database.UserManager.FindById(token);
            if (user != null)
            {
                if (user.Email == email)
                {
                    user.EmailConfirmed = true;
                    database.UserManager.Update(user);
                    claim = database.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
            return claim;
        }

        public ClaimsIdentity Authenticate(UserDTO userDto)
        {
            //if(userDto.Email == "admin" && userDto.Password == "111111")
            //{
            //    FirstInitialize();
            //}
            ClaimsIdentity claim = null;
            ApplicationUser user = database.UserManager.Find(userDto.Email, userDto.Password);
            if (user != null)
            {
                if(!user.EmailConfirmed)
                {
                    throw new Exception(EMAIL_NOT_CONFIRM_ERROR);
                }
                claim = database.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
                return claim;
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
                    new ApplicationRole {Name = "user" }
                };
                foreach (ApplicationRole role in roles)
                {
                    database.RoleManager.Create(role);
                }
                UserDTO user = new UserDTO
                {
                    Email = "admin",
                    Role = "admin",
                    UserName = "Administrator",
                    Password = "111111"
                };
                OperationDetails op =  Create(user, null);
                ApplicationUser u = database.UserManager.Find(user.Email, user.Password);
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
