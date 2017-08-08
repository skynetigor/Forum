using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.WEB.Attributes;
using Forum.WEB.Models;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    [UnAuthorize]
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

        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                ClaimsIdentity claim = service.Authenticate(new UserDTO
                {
                    Name = model.UserName,
                    Email = model.UserName,
                    Password = model.Password
                });
                AuthenticationManager.SignIn(claim);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetModel()
        {
            return View();
        }

        public ActionResult Registration()
        {
            string s = User.Identity.Name;
            return View();
        }

        public ActionResult ConfirmeEmail(int token, string email)
        {
            ClaimsIdentity claim = service.ConfirmEmail(token, email);
            AuthenticationManager.SignIn(claim);
            return RedirectToAction("Login");
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
            try
            {
                string url = Url.Action("ConfirmeEmail", "Account", null, Request.Url.Scheme) + "?token={0}&email={1}";
                OperationDetails opdet = service.Create(user, url);
                if (!opdet.Succedeed)
                {
                    ModelState.AddModelError(string.Empty, opdet.Message);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }
    }
}