using Forum.Core.BLL.Infrastructure;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.DAL.Entities.Identity;
using Forum.WEB.Attributes;
using Forum.WEB.Models.ContentViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class CommentController : Controller
    {
        private IContentService<Topic> topicService;
        private IContentService<Comment> commentService;
        public CommentController(IContentService<Topic> topicService, IContentService<Comment> commentService)
        {
            this.topicService = topicService;
            this.commentService = commentService;
        }

        [MyAllowAnonymous]
        public ActionResult Index(int? currentTopic)
        {
            var topic = topicService.FindById((int)currentTopic);
            return View(topic);
        }

        [MyAuthorize(Permission = BlockType.Comment)]
        public ActionResult Update(int? id, int? currentId)
        {
            if (id !=null)
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
            return View(new CommentViewModel {
                TopicId = (int)currentId
            });
        }

        [MyAuthorize(Permission = BlockType.Comment)]
        [HttpPost]
        public ActionResult Update(CommentViewModel viewModel)
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
            if(viewModel.Id == 0)
            {
                commentService.Create(user, comment);
            }
            else
            {
                commentService.Update(user, comment);
            }
            return RedirectToAction("index", new { currentTopic = viewModel.TopicId });
        }
    }
}