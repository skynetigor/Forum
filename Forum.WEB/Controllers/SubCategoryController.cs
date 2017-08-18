using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Attributes;
using Forum.WEB.Models;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class SubCategoryController : Controller
    {
        const int PAGE_SIZE = 10;

        private IContentService<SubCategory> subCategoryService;
        private IContentService<Category> categoryService;

        public SubCategoryController(IContentService<SubCategory> subCategoryService, IContentService<Category> categoryService)
        {
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? Id, int page = 1)
        {
            if (Id != null)
            {
                var categories = categoryService.Get();
                ViewBag.Categories = categories;

                var subCategory = subCategoryService.FindById((int)Id);
                PagingViewModel<Topic> viewModel = new PagingViewModel<Topic>(page, PAGE_SIZE, subCategory.Topics)
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name
                };
                if(page > viewModel.PageInfo.TotalPages)
                {
                    return RedirectToAction("index", new { id = Id, page = viewModel.PageInfo.TotalPages });
                }
                return View(viewModel);
            }
            return null;
        }

        [Authorize(Roles = "admin,moderator")]
        public ActionResult Update(int categoryId = 0, int Id = 0)
        {
            var selectList = new SelectList(categoryService.Get(), "Id", "Name");
            if (Id != 0)
            {
                var subcategory = subCategoryService.FindById(Id);
                var categoryViewModel = new SubCategoryViewModel
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Description = subcategory.Description,
                    Categories = selectList,
                    CategoryName = subcategory.Category.Name
                };
                return PartialView(categoryViewModel);
            }
            var catViewModel = new SubCategoryViewModel
            {
                CategoryName = categoryService.FindById(categoryId).Name,
                CategoryId = categoryId,
                Categories = selectList
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