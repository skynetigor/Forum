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
        private IBlockService blockService;
        public AdminController(IAdminService adminService, IBlockService blockService)
        {
            this.adminService = adminService;
            this.blockService = blockService;
        }
        [Authorize(Roles = "admin")]
        public ActionResult UserList()
        {
            var users = adminService.GetUsers();
            return View(users);
        }

        public ActionResult Block(int? userid)
        {
            var block = blockService.GetUserBlockByUserId((int)userid);
            return View(block);
        }

        [HttpPost]
        public ActionResult Block(BlockDTO block)
        {
            adminService.Block(block);
            return RedirectToAction("userlist");
        }
    }
}