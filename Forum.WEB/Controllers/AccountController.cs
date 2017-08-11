using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.WEB.Attributes;
using Forum.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    
    public class AccountController : Controller
    {
        private IAuthManager service;
        public AccountController(IAuthManager service)
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
            return RedirectToAction("login");
        }

        [UnAuthorize]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [UnAuthorize]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                var claim = service.Authenticate(model.Login, model.Password);
                AuthenticationManager.SignIn(claim);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return RedirectToAction("index", "category");
        }

        [UnAuthorize]
        [HttpGet]
        public ActionResult GetModel()
        {
            return View();
        }

        [UnAuthorize]
        public ActionResult Registration()
        {
            var s = User.Identity.Name;
            return View();
        }

        [UnAuthorize]
        public ActionResult ConfirmeEmail(int token, string email)
        {
            var claim = service.ConfirmEmail(token, email);
            AuthenticationManager.SignIn(claim);
            return RedirectToAction("login");
        }

        [UnAuthorize]
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            try
            {
                var url = Url.Action("ConfirmeEmail", "Account", null, Request.Url.Scheme) + "?token={0}&email={1}";
                var user = new UserDTO
                {
                    Email = model.Email,
                };
                var opdet = service.Create(user, model.Password, url);
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