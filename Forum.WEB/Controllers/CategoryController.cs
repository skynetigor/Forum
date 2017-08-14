using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using Forum.WEB.Attributes;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
namespace Forum.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private IContentService<CategoryDTO> categoryService;
        private IBlockService blockService;
        private IAuthManager userService
        {
            get { return HttpContext.GetOwinContext().GetUserManager<IAuthManager>(); }
        }

        public CategoryController(IContentService<CategoryDTO> categoryService, IBlockService blockService)
        {
            this.categoryService = categoryService;
            this.blockService = blockService;
        }
        
        [MyAllowAnonymous]
        public ActionResult Index()
        {
            return View(categoryService.Get());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Update(int? Id)
        {
            var categoryViewModel = new CategoryViewModel();
            var users = userService.GetUsers();
            categoryViewModel.Users = new SelectList(users, "Id", "Name" );
            if(Id != null)
            {
                var category = this.categoryService.FindById((int)Id);
                categoryViewModel.Id = category.Id;
                categoryViewModel.Name = category.Name;
                categoryViewModel.Description = category.Title;
                categoryViewModel.ModeratorId = category.ModeratorId;
                categoryViewModel.ModeratorName = category.ModeratorName;
            }
            return View(categoryViewModel);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Update(CategoryViewModel viewModel)
        {
            var user = new UserDTO
            {
                Email = User.Identity.Name,
                Id = User.Identity.GetUserId<int>()
            };
            var cat = new CategoryDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Title = viewModel.Description,
                ModeratorId = viewModel.ModeratorId
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