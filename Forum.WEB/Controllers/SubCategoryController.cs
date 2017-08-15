using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class SubCategoryController : Controller
    {
        private IContentService<SubCategory> subCategoryService;
        private IContentService<Category> categoryService;

        public SubCategoryController(IContentService<SubCategory> subCategoryService, IContentService<Category> categoryService)
        {
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        [Authorize(Roles = "admin,moderator")]
        public ActionResult Update(int? Id)
        {
            if (Id != null)
            {
                var subcategory = subCategoryService.FindById((int)Id);
                var categoryViewModel = new SubCategoryViewModel
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Description = subcategory.Description,
                    Categories = new SelectList(categoryService.Get(), "Id", "Name")
                };
                return PartialView(categoryViewModel);
            }
            var catViewModel = new SubCategoryViewModel
            {
                Categories = new SelectList(categoryService.Get(), "Id", "Name")
            };
            return PartialView(catViewModel);
        }

        [Authorize(Roles = "admin,moderator")]
        [HttpPost]
        public ActionResult Update(SubCategoryViewModel viewModel)
        {
            var user = new AppUser
            {
                Id = User.Identity.GetUserId<int>(),
                Email = User.Identity.Name
            };
            var category = new Category {
                Id = viewModel.CategoryId
            };
            var subCategory = new SubCategory
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Id = viewModel.Id,
                Category = category
            };
            if (subCategory.Id == 0)
            {
                subCategoryService.Create(user, subCategory);
            }
            else
            {
                subCategoryService.Update(user, subCategory);
            }
            return RedirectToAction("Index", "Category");
        }
    }
}