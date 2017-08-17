using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Attributes;
using Forum.WEB.Helpers;
using Forum.WEB.Models;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class TopicController : Controller
    {
        const int PAGE_SIZE = 10;
        private IContentService<Topic> topicService;
        private IContentService<SubCategory> subCategoryService;
        private IContentService<Category> categoryService;
        public TopicController(IContentService<Category> categoryService,IContentService<Topic> topicService, IContentService<SubCategory> subCategoryService)
        {
            this.topicService = topicService;
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? subCategoryId, int page = 1)
        {
            if (subCategoryId != null)
            {
                var categories = categoryService.Get();
                ViewBag.Categories = categories;

                var subCategory = subCategoryService.FindById((int)subCategoryId);
                PagingViewModel<Topic> viewModel = new PagingViewModel<Topic>(page,PAGE_SIZE,subCategory.Topics)
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name
                };
                return View(viewModel);
            }
            return null;
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        public ActionResult Update(int? id, int? currentId)
        {
            var categories = categoryService.Get();
            ViewBag.Categories = categories;
            if (id != null)
            {
                var topic = topicService.FindById((int)currentId);
                return View(new TopicViewModel {
                    Description = topic.Description,
                    Id = (int)id,
                    Message = topic.Message,
                    SubCategoryId = topic.SubCategory.Id,
                    SubCategoryName = topic.SubCategory.Name
                });
            }
            return View(new TopicViewModel {
                SubCategoryId = (int)currentId,
            });
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        [HttpPost]
        public ActionResult Update(TopicViewModel topic)
        {
            var user = new AppUser
            {
                Id = User.Identity.GetUserId<int>()
            };
            var subcategory = new SubCategory
            {
                Id = topic.SubCategoryId
            };
            var t = new Topic
            {
                Id = topic.Id,
                Message = topic.Message,
                Description = topic.Description,
                User = user,
                SubCategory = subcategory
            };
            if (topic.Id == 0)
            {
                topicService.Create(user, t);
            }
            else
            {
                topicService.Update(user, t);
            }
            return RedirectToAction("index", new { subCategoryId = topic.SubCategoryId });
        }
    }
}