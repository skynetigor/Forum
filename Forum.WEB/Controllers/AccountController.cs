using Forum.Core.BLL.Interfaces;
using Forum.Core.BLL.Model;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
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
                    var user = new RegistrationModel
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password
                    };
                    var opdet = userService.CreateAccount(user, url);
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
        public ActionResult Settings()
        {
            var user = userService.FindById(User.Identity.GetUserId<int>());
            var account = new AccountSettingsViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
            };
            return View(account);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Settings(AccountSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new RegistrationModel
                {
                    Email = model.Email,
                    Password = model.FirstPassword,
                    UserName = model.UserName
                };
                var result = userService.ChangeAccountSettings(User.Identity.GetUserId<int>(),model.OldPassword, account);
                if (!result.Succedeed)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
            }
            return View(new AccountSettingsViewModel());
        }
    }
}