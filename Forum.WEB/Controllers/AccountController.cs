using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{

    public class AccountController : Controller
    {
        const string PASS_CHANGED = "Пароль успешно изменен";
        const string EMAIL_SEND = "Вам отправлено письмо с инструкциями для активации вашего аккаунта.";
        const string LOGIN_OR_PASS_FAILED = "Неверный логин или пароль.";
        private IAuthManager userService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IAuthManager>(); }
        }

        private IAuthenticationManager authenticationManager
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
            authenticationManager.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult Login(string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
            {
               return this.RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                var claim = userService.Authenticate(model.Login, model.Password);
                if(claim == null)
                {
                    ModelState.AddModelError("", LOGIN_OR_PASS_FAILED);
                    return View();
                }
                else
                { 
                authenticationManager.SignIn(claim);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View();
            }
            return RedirectToAction("index", "category");
        }

        public ActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Category");
            }
            var s = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Category");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var url = Url.Action("ConfirmeEmail", "Account", null, Request.Url.Scheme) + "?token={0}&email={1}";
                    var user = new AppUser
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };
                    var opdet = userService.Create(user, model.Password, url);
                    if (!opdet.Succedeed)
                    {
                        ModelState.AddModelError(string.Empty, opdet.Message);
                    }
                    else
                    {
                        ViewBag.Message = EMAIL_SEND;
                        return View("Message");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View();
        }

        public ActionResult ConfirmeEmail(int token, string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Category");
            }
            var claim = userService.ConfirmEmail(token, email);
            authenticationManager.SignIn(claim);
            return RedirectToAction("login");
        }

        //[HttpPost]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Id = User.Identity.GetUserId<int>()
                };
                var result = userService.ChangePassword(user, model.OldPassword, model.FirstPassword);
                if(!result.Succeeded)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                ViewBag.Message = PASS_CHANGED;
            }
            return View(new ChangePasswordViewModel());
        }
    }
}