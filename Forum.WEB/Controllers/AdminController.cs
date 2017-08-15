using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Attributes;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService adminService;
        private IBlockService blockService;
        private IAuthManager userService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IAuthManager>(); }
        }
        public AdminController(IAdminService adminService, IBlockService blockService)
        {
            this.adminService = adminService;
            this.blockService = blockService;
        }
        [MyAuthorize(Roles = "admin")]
        public ActionResult UserList()
        {
            var users = userService.GetUsers();
            return View(users);
        }

        [MyAuthorize(Roles = "admin")]
        public ActionResult Block(int? userid)
        {
            var block = blockService.GetUserBlockByUserId((int)userid);
            return View(block);
        }

        [MyAuthorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Block(Block block)
        {
            adminService.Block(block);
            return RedirectToAction("userlist");
        }
    }
}