using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Identity;
using Forum.Core.DAL.Interfaces;
using Forum.WEB.Attributes;
using Forum.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class AdminController : Controller
    {
        const int PAGINGS = 10;
        private IAdminService adminService;
        private IBlockService blockService;
        private IGenericRepository<Log> logRepository;
        private IAuthManager userService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IAuthManager>(); }
        }
        public AdminController(IAdminService adminService, IBlockService blockService, IGenericRepository<Log> logRepository)
        {
            this.adminService = adminService;
            this.blockService = blockService;
            this.logRepository = logRepository;
        }

        [MyAuthorize(Roles = "admin")]
        public ActionResult UserList()
        {
            var users = userService.GetUsers();
            return View(users);
        }

        public ActionResult Logs(int page = 1)
        {
            var logs = logRepository.Get();
            PagingViewModel<Log> paging = new PagingViewModel<Log>(page, PAGINGS, logs);
            return View(paging);
        }

        [MyAuthorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Block(BlockViewModel blockModel)
        {
            var user = new AppUser
            {
                Id = blockModel.UserId,
                IsAccessBlocked = blockModel.IsAccess,
                IsCommentBlocked = blockModel.IsComment,
                IsTopicBlocked = blockModel.IsTopic
            };
            
            this.adminService.Block(user);
            return RedirectToAction("userlist");
        }
    }
}