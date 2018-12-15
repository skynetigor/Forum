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
        public TopicController(IContentService<Category> categoryService, IContentService<Topic> topicService, IContentService<SubCategory> subCategoryService)
        {
            this.topicService = topicService;
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? Id, bool last = false, int page = 1)
        {
            var categories = categoryService.Get();
            ViewBag.Categories = categories;
            ViewBag.Page = page;
            var topic = topicService.FindById((int)Id);
            PagingViewModel<Comment> viewModel = null;
            if (last)
            {
                viewModel = new PagingViewModel<Comment>(PAGE_SIZE, topic.Comments)
                {
                    Id = topic.Id,
                    Name = topic.Description
                };
            }
            else
            {
                viewModel = new PagingViewModel<Comment>(page, PAGE_SIZE, topic.Comments)
                {
                    Id = topic.Id,
                    Name = topic.Description
                };
            }


            return View(viewModel);
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        public ActionResult Update(int? id, int? currentId)
        {
            var categories = categoryService.Get();
            ViewBag.Categories = categories;
            if (id != null)
            {
                var topic = topicService.FindById((int)currentId);
                return View(new TopicViewModel
                {
                    Description = topic.Description,
                    Id = (int)id,
                    Message = topic.Message,
                    SubCategoryId = topic.SubCategory.Id,
                    SubCategoryName = topic.SubCategory.Name
                });
            }
            return View(new TopicViewModel
            {
                SubCategoryId = (int)currentId,
            });
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        [HttpPost]
        public string Update(TopicViewModel topic)
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
            return Url.Action("Index", new { Id = t.Id });
        }
    }
}