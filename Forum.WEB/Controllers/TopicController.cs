using Forum.BLL.DTO;
using Forum.BLL.DTO.Content;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.WEB.Attributes;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class TopicController : Controller
    {
        private ITopicService topicService;
        private IContentService<SubCategoryDTO> subCategoryService;
        public TopicController(ITopicService topicService, IContentService<SubCategoryDTO> subCategoryService)
        {
            this.topicService = topicService;
            this.subCategoryService = subCategoryService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? subCategoryId)
        {
            if (subCategoryId != null)
            {
                var topics = topicService.GetTopicsBySubCategoryId((int)subCategoryId);
                ViewBag.subCategoryId = subCategoryId;
                return View(topics);
            }
            return null;
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        public ActionResult Update(int? id, int? currentId)
        {
            if (id != null)
            {
                var topic = topicService.FindById((int)id);
                return View(topic);
            }
            return View(new TopicDTO {
                SubCategoryId = (int)currentId
            });
        }

        [MyAuthorize(Permission = BlockType.Topic)]
        [HttpPost]
        public ActionResult Update(TopicDTO topic)
        {
            var user = new UserDTO
            {
                Email = User.Identity.Name,
                Id = User.Identity.GetUserId<int>()
            };
            if (topic.Id == 0)
            {
                topicService.Create(user, topic);
            }
            else
            {
                topicService.Update(user, topic);
            }
            return RedirectToAction("index", new { subCategoryId = topic.SubCategoryId });
        }
    }
}