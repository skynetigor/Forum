using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using Forum.WEB.Models.Category;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        
        public ActionResult Index()
        {
            return View(categoryService.GetCategories());
        }

        public ActionResult UpdateCategory(int? categoryId)
        {
            ViewBag.Resourse = Url.Action("UpdateCategory");
            if(categoryId != null)
            {
                var category = categoryService.FindCategoryById((int)categoryId);
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
        public ActionResult UpdateCategory(CategoryViewModel category)
        {
            var user = new UserDTO
            {
                 Id = User.Identity.GetUserId<int>(),
                 Name = User.Identity.Name
            };
            var cat = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Title = category.Title
            };
            if (cat.Id == 0)
            {
                categoryService.CreateCategory(user, cat);
            }
            else
            {
                categoryService.UpdateCategory(user, cat);
            }
            return RedirectToAction("Index");
        }
    }
}