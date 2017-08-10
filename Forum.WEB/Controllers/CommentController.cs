using Forum.BLL.DTO;
using Forum.BLL.DTO.Content;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.WEB.Controllers
{
    public class CommentController : Controller
    {
        private ITopicService topicService;
        private ICommentService commentService;

        public CommentController(ITopicService topicService, ICommentService commentService)
        {
            this.topicService = topicService;
            this.commentService = commentService;
        }

        public ActionResult Index(int? currentTopic)
        {
            var comments = commentService.GetCommentsByTopicId((int)currentTopic);
            ViewBag.TopicId = currentTopic;
            return View(comments);
        }

        public ActionResult Update(int? id, int? currentId)
        {
            if(id !=null)
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