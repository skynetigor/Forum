using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.WEB.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class AccountController : Controller
    {
        IUserService service;
        public AccountController(IUserService service)
        {
            this.service = service;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Account

        public ActionResult Login(string returnUrl)
        {
            string s = User.Identity.Name;
            ClaimsIdentity c = service.Authenticate(new UserDTO
            {
                Name = "Igor Panasiuk",
                Email = "skynet_95@mail.ru",
                Password = "123456"
            });
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(c);
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            
            return View();
        }

        public ActionResult Registration()
        {
            string s = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            UserDTO user = new UserDTO
            {
                Name = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Role = "User"
            };
            OperationDetails opdet = service.Create(user);
            if(!opdet.Succedeed)
            {
                ModelState.AddModelError("", opdet.Message);
            }
            return View();
        }
    }
}