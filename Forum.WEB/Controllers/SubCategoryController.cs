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
        private IContentService<SubCategoryDTO> subCategoryService;
        private IContentService<CategoryDTO> categoryService;

        public SubCategoryController(IContentService<SubCategoryDTO> subCategoryService, IContentService<CategoryDTO> categoryService)
        {
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View(subCategoryService.Get());
        }

        public ActionResult UpdateSubCategory(int? subcategoryId)
        {
            if (subcategoryId != null)
            {
                SubCategoryDTO subcategory = subCategoryService.FindById((int)subcategoryId);
                var categoryViewModel = new SubCategoryViewModel
                {
                    Id = subcategory.Id,
                    Name = subcategory.Name,
                    Title = subcategory.Title,
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

        [HttpPost]
        public ActionResult UpdateSubCategory(SubCategoryViewModel categoryvm)
        {
            UserDTO user = new UserDTO
            {
                Id = User.Identity.GetUserId<int>(),
                Name = User.Identity.Name
            };
            var category = new CategoryDTO {
                Id = categoryvm.CategoryId
            };
            var subCategory = new SubCategoryDTO
            {
                Name = categoryvm.Name,
                Title = categoryvm.Title,
                Id = categoryvm.Id,
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