using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Attributes;
using Forum.WEB.Models;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
namespace Forum.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 3;
        private IContentService<Category> categoryService;
        private IBlockService blockService;
        private IAuthManager userService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IAuthManager>(); }
        }

        public CategoryController(IContentService<Category> categoryService, IBlockService blockService)
        {
            this.categoryService = categoryService;
            this.blockService = blockService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int page = 1)
        {
            var categories = categoryService.Get();
            PagingViewModel<Category> viewModel = new PagingViewModel<Category>(page, PAGE_SIZE, categories);
            ViewBag.Categories = categories;
            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Update(int? Id)
        {
            var categoryViewModel = new CategoryViewModel();
            var users = userService.GetUsers();
            categoryViewModel.Users = new SelectList(users, "Id", "UserName");

            if (Id != null)
            {
                var category = this.categoryService.FindById((int)Id);
                categoryViewModel.Id = category.Id;
                categoryViewModel.Name = category.Name;
                categoryViewModel.Description = category.Description;
                if (category.Moderator != null)
                {
                    categoryViewModel.ModeratorId = category.Moderator.Id;
                    categoryViewModel.ModeratorName = category.Moderator.UserName;
                }
            }
            return View(categoryViewModel);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Update(CategoryViewModel viewModel)
        {
            var user = new AppUser
            {
                Id = viewModel.ModeratorId
            };
            var cat = new Category
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Moderator = user
            };
            if (cat.Id == 0)
            {
                categoryService.Create(user, cat);
            }
            else
            {
                categoryService.Update(user, cat);
            }
            return RedirectToAction("Index");
        }
    }
}