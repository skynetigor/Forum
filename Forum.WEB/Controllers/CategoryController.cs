using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using Forum.WEB.Attributes;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class CategoryController : Controller, IContentController<CategoryViewModel>
    {
        private IContentService<CategoryDTO> categoryService;
        private IBlockService blockService;
        public CategoryController(IContentService<CategoryDTO> categoryService, IBlockService blockService)
        {
            this.categoryService = categoryService;
            this.blockService = blockService;
        }
        
        [MyAuthorize]
        public ActionResult Index()
        {
            var block = blockService.GetUserStatusByUserId(User.Identity.GetUserId<int>());
            if (block.IsAccess)
            {
                return View("Error", (object)block.Message);
            }
            return View(categoryService.Get());
        }

        public ActionResult Update(int? Id)
        {
            ViewBag.Resourse = Url.Action("UpdateCategory");
            if(Id != null)
            {
                var category = this.categoryService.FindById((int)Id);
                var categoryViewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title
                };
                return PartialView(categoryViewModel);
            }
            return PartialView(new CategoryViewModel());
        }

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
                Title = viewModel.Title
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