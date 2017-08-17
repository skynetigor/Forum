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
using Forum.Core.BLL.Model;

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
        const string REGISTRATION_SUCCESS = "Регистрация успешно пройдена";
        const string REGISTRATION_FAILED = "Пользователь с таким {0} уже существует";
        const string BLOCK = "У вас ограниченен доступ к ресурсу. За информацией Обратитесь к администратору форума. ";


        private IIdentityProvider identity;
        private IConfirmedEmailSender emailSender;
        private IBlockService blockService;

        public AuthManager(IIdentityProvider identity, IConfirmedEmailSender emailSender, IBlockService blockService)
        {
            this.emailSender = emailSender;
            this.identity = identity;
            this.blockService = blockService;
            FirstInitialize();
        }

        public AppUser FindById(int Id)
        {
            return identity.UserManager.FindById(Id);
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

        public OperationDetails CreateAccount(RegistrationModel user, string confirmeUrl)
        {
            var appuser = identity.UserManager.FindByEmail(user.Email);
            var login = identity.UserManager.FindByName(user.UserName);
            if (appuser == null && login == null)
            {
                appuser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                var result = identity.UserManager.Create(appuser, user.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault());
                identity.UserManager.AddToRole(appuser.Id, "user");
                if (!string.IsNullOrEmpty(confirmeUrl))
                    emailSender.SendConfirmationMessage(GenerateMessage(appuser, confirmeUrl), PASSWORD);
                return new OperationDetails(true, REGISTRATION_SUCCESS);
            }
            else
            {
                return new OperationDetails(false, string.Format(REGISTRATION_FAILED, "логином или почтовым адресом"));
            }
        }

        public ClaimsIdentity Authenticate(string login, string password)
        {
            var user = identity.UserManager.Find(login, password);
            if (user != null)
            {
                if(user.IsAccessBlocked)
                { 
                        throw new Exception(BLOCK);
                    
                }
                if (!user.EmailConfirmed)
                {
                    throw new Exception(EMAIL_NOT_CONFIRM_ERROR);
                }
                return identity.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return null;
        }

        public OperationDetails ChangeAccountSettings(int userId, string currentPassword, RegistrationModel model)
        {
            try {
                var user = identity.UserManager.FindById(userId);
                var failUserEmail = identity.UserManager.FindByEmail(model.Email);
                var failUserName = identity.UserManager.FindByName(model.UserName);
               
                    if (user.Id != failUserEmail.Id && failUserEmail != null)
                    {
                        return new OperationDetails(false, string.Format(REGISTRATION_FAILED, "почтовым адресом"));
                    }
                    if (user.Id != failUserName.Id && failUserEmail != null)
                    {
                        return new OperationDetails(false, string.Format(REGISTRATION_FAILED, "логином"));
                    }
            
            user.Email = model.Email;
            user.UserName = model.UserName;
            identity.UserManager.Update(user);
            var result = identity.UserManager.ChangePassword(user.Id, currentPassword, model.Password);
                if(!result.Succeeded)
                {
                    string errors = string.Empty;
                    foreach(string error in result.Errors)
                    {
                        errors += error + " ";
                    }
                    throw new Exception(errors);
                }
            return new OperationDetails(true, "Данные успешно изменены.");
            }
            catch(Exception e)
            {
                return new OperationDetails(false, e.Message);
            }
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
