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
    public class SubCategoryController : Controller
    {
        private ICategoryService categoryService;

        public SubCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View(categoryService.GetCategories());
        }

        public ActionResult UpdateSubCategory(int? subcategoryId)
        {
            if (subcategoryId != null)
            {
                SubCategoryDTO subcategory = categoryService.FindSubCategoryById((int)subcategoryId);
                var categoryViewModel = new SubCategoryViewModel
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Title = subcategory.Title,
                    Categories = new SelectList(categoryService.GetCategories(), "Id", "Name")
                };
                return PartialView(categoryViewModel);
            }
            var catViewModel = new SubCategoryViewModel
            {
                Categories = new SelectList(categoryService.GetCategories(), "Id", "Name")
            };
            return PartialView(catViewModel);
        }

        [HttpPost]
        public ActionResult UpdateSubCategory(SubCategoryViewModel categoryvm)
        {
            UserDTO user = new UserDTO
            {
                Id = User.Identity.GetUserId<int>(),
                Name = User.Identity.Name
            };
            var category = categoryService.FindCategoryById(categoryvm.CategoryId);
            var subCategory = new SubCategoryDTO
            {
                Name = categoryvm.Name,
                Title = categoryvm.Title,
                Id = categoryvm.Id,
                Category = category
            };
            if (subCategory.Id == 0)
            {
                categoryService.CreateSubCategory(user, category, subCategory);
            }
            else
            {
                categoryService.UpdateSubCategory(user, subCategory);
            }
            return RedirectToAction("Index", "Category");
        }
    }
}