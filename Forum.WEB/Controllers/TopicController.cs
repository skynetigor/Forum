using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Attributes;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class TopicController : Controller
    {
        private IContentService<Topic> topicService;
        private IContentService<SubCategory> subCategoryService;
        public TopicController(IContentService<Topic> topicService, IContentService<SubCategory> subCategoryService)
        {
            this.topicService = topicService;
            this.subCategoryService = subCategoryService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? subCategoryId)
        {
            if (subCategoryId != null)
            {
                var topic = subCategoryService.FindById((int)subCategoryId);
                return View(topic);
            }
            return null;
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        public ActionResult Update(int? id, int? currentId)
        {
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