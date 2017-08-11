using Forum.BLL.DTO;
using Forum.BLL.DTO.Content;
using Forum.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class CommentController : Controller
    {
        private ITopicService topicService;
        private ICommentService commentService;
        private IBlockService blockService;
        public CommentController(ITopicService topicService, ICommentService commentService, IBlockService blockService)
        {
            this.topicService = topicService;
            this.commentService = commentService;
            this.blockService = blockService;
        }

        public ActionResult Index(int? currentTopic)
        {
            var block = blockService.GetUserStatusByUserId(User.Identity.GetUserId<int>());
            if (block.IsAccess)
            {
                return View("Error", (object)block.Message);
            }
            var comments = commentService.GetCommentsByTopicId((int)currentTopic);
            ViewBag.TopicId = currentTopic;
            return View(comments);
        }

        public ActionResult Update(int? id, int? currentId)
        {
            var block = blockService.GetUserStatusByUserId(User.Identity.GetUserId<int>());
            if (block.IsComment || block.IsAccess)
            {
                return View("Error", (object)block.Message);
            }
            if (id !=null)
            {
                var comment = commentService.FindById((int)id);
                return View(comment);
            }
            
            return View(new CommentDTO {
                Date = DateTime.Now,
                TopicId = (int)currentId
            });
        }

        [HttpPost]
        public ActionResult Update(CommentDTO viewModel)
        {
            var user = new UserDTO
            {
                Id = User.Identity.GetUserId<int>()
            };
            if(viewModel.Id == 0)
            {
                commentService.Create(user, viewModel);
            }
            else
            {
                commentService.Update(user, viewModel);
            }
            return RedirectToAction("index", new { currentTopic = viewModel.TopicId });
        }
    }
}