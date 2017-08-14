using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using Forum.WEB.Models.ContentViewModels;
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
                    Description = subcategory.Title,
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
            var user = new UserDTO
            {
                Id = User.Identity.GetUserId<int>(),
                Email = User.Identity.Name
            };
            var category = new CategoryDTO {
                Id = viewModel.CategoryId
            };
            var subCategory = new SubCategoryDTO
            {
                Name = viewModel.Name,
                Title = viewModel.Description,
                Id = viewModel.Id,
                CategoryId = category.Id,
                CategoryName = category.Name
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