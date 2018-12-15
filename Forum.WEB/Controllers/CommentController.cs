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
    public class CommentController : Controller
    {
        const int PAGE_SIZE = 10;
        private IContentService<Topic> topicService;
        private IContentService<Comment> commentService;
        private IContentService<Category> categoryService;
        public CommentController(IContentService<Category> categoryService, IContentService<Topic> topicService, IContentService<Comment> commentService)
        {
            this.topicService = topicService;
            this.commentService = commentService;
            this.categoryService = categoryService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? currentTopic, int page = 1)
        {
            var categories = categoryService.Get();
            ViewBag.Categories = categories;
            var topic = topicService.FindById((int)currentTopic);
            PagingViewModel<Comment> viewModel = new PagingViewModel<Comment>(page, PAGE_SIZE, topic.Comments)
            {
                Id = topic.Id,
                Name = topic.Description
            };
            return View(viewModel);
        }

        [MyAuthorize(Permission = BlockType.Comment)]
        public ActionResult Update(int? id, int? currentId, int? returnPageId)
        {
            var categories = categoryService.Get();
            ViewBag.Categories = categories;

            ViewBag.ReturnPageId = returnPageId;

            if (id != null)
            {
                var comment = commentService.FindById((int)id);
                var model = new CommentViewModel
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    TopicId = comment.Topic.Id
                };
                return View(model);
            }
            return View(new CommentViewModel
            {
                TopicId = (int)currentId
            });
        }

        [MyAuthorize(Permission = BlockType.Comment)]
        [HttpPost]
        public ActionResult Update(CommentViewModel viewModel, int? returnPageId)
        {
            var user = new AppUser
            {
                Id = User.Identity.GetUserId<int>()
            };
            var topic = new Topic
            {
                Id = viewModel.TopicId
            };
            var comment = new Comment
            {
                Message = viewModel.Message,
                Id = viewModel.Id,
                Topic = topic,
                User = user
            };
            if (viewModel.Id == 0)
            {
                commentService.Create(user, comment);
            }
            else
            {
                commentService.Update(user, comment);
            }

            object routeValues;

            if (returnPageId != null)
            {
                routeValues = new { id = viewModel.TopicId, page = returnPageId };
            }
            else
            {
                routeValues = new { id = viewModel.TopicId };
            }

            return RedirectToAction("Index", "Topic", routeValues);
        }
    }
}