using Forum.BLL.DTO;
using Forum.BLL.Interfaces;
using Forum.WEB.Attributes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService adminService;
        private IUserService userService;
        public AdminController(IAdminService adminService, IUserService userService)
        {
            this.adminService = adminService;
            this.userService = userService;
        }

        public ActionResult UserList()
        {
            var users = userService.GetUsers();
            return View(users);
        }

        [MyAuthorize(Roles = "admin")]
        [HttpPost]
        public ActionResult BlockUser(int userid, string message)
        {
            var admin = new UserDTO
            {
                Id = User.Identity.GetUserId<int>()
            };
            var user = new UserDTO
            {
                Id = userid
            };
            adminService.BlockUser(admin, user, message);
            return View();
        }

        [HttpPost]
        [MyAuthorize(Roles = "admin")]
        public ActionResult UnBlockUser(int userid, string message)
        {
            var admin = new UserDTO
            {
                Id = User.Identity.GetUserId<int>()
            };
            var user = new UserDTO
            {
                Id = userid
            };
            adminService.UnBlockUser(admin, user, message);
            return View();
        }
    }
}