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

namespace Forum.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork database = new IdentityUnitOfWork("ApplicationContext");

        public OperationDetails Create(UserDTO userDto)
        {
            ApplicationUser user = null;
            try
            {
                user = database.UserManager.FindByEmail(userDto.Email);

                if (user == null)
                {
                    user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                    var result = database.UserManager.Create(user, userDto.Password);
                    if (result.Errors.Count() > 0)
                        return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                    // добавляем роль
                    database.UserManager.AddToRole(user.Id, userDto.Role);
                    // создаем профиль клиента
                    ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                    database.ClientManager.Create(clientProfile);
                    database.Save();
                    return new OperationDetails(true, "Регистрация успешно пройдена", "");
                }
                else
                {
                    return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public ClaimsIdentity Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = database.UserManager.Find(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = database.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
