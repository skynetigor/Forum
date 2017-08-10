using Forum.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Forum.BLL.DTO;
using Forum.BLL.DTO.Content;
using Forum.BLL.Infrastructure;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using Forum.DAL.Entities.Topics;
using System;

namespace Forum.BLL.Services
{
    public class CommentService : AbstractContentService<CommentDTO>,ICommentService
    {
        private IGenericRepository<Comment> commentRepository;
        private IGenericRepository<Topic> topicRepository;
        private IUnitOfWork identity;

        public CommentService(IGenericRepository<Comment> commentRepository, IGenericRepository<Topic> topicRepository, IUnitOfWork identity)
        {
            this.commentRepository = commentRepository;
            this.topicRepository = topicRepository;
            this.identity = identity;
        }

        private CommentDTO Map(Comment comment)
        {
            var dto = new CommentDTO
            {
                Id = comment.Id,
                Date = comment.Date,
                Message = comment.Message,
                UserName = comment.User.UserName
            };
            return dto;
        }

        public override IEnumerable<CommentDTO> Get()
        {
            var comments = commentRepository.Get();
            var tdolist = new List<CommentDTO>();
            foreach(var c in comments)
            {
                tdolist.Add(Map(c));
            }
            return tdolist;
        }

        public override CommentDTO FindById(int id)
        {
            var comment = commentRepository.FindById(id);
            return Map(comment);
        }

        public IEnumerable<CommentDTO> GetCommentsByTopicId(int id)
        {
            var topic = topicRepository.FindById(id);
            var commentsList = new List<CommentDTO>();
            foreach(Comment c in topic.Comments)
            {
                commentsList.Add(Map(c));
            }
            return commentsList;
        }

        protected override OperationDetails CreateContent(UserDTO user, CommentDTO content)
        {
            var appuser = identity.UserManager.Users.First();
            var topic = topicRepository.FindById(content.TopicId);
            if(!appuser.IsBlocked  && !topic.IsBlocked)
            {
                var comment = new Comment
                {
                    Date = DateTime.Now,
                    Message = content.Message,
                    User = appuser,
                };
                topic.Comments.Add(comment);
                topicRepository.Update(topic);
            }
            return new OperationDetails(false, ACCESS_ERROR, string.Empty);
        }

        protected override OperationDetails UpdateContent(UserDTO user, CommentDTO content)
        {
            var appuser = identity.UserManager.Users.First();
            var topic = topicRepository.FindById(content.TopicId);
            if (!appuser.IsBlocked && !topic.IsBlocked && topic.User == appuser)
            {
                var comment = commentRepository.FindById(content.Id);
                comment.Date = content.Date;
                comment.Message = content.Message;
                comment.Topic = topic;
                commentRepository.Update(comment);
            }
            return new OperationDetails(false, ACCESS_ERROR, string.Empty);
        }

        protected override OperationDetails DeleteContent(UserDTO user, CommentDTO content)
        {
            var appuser = identity.UserManager.Users.First();
            var topic = topicRepository.FindById(content.TopicId);
            if (!appuser.IsBlocked && !topic.IsBlocked)
            {

            }
            return new OperationDetails(false, ACCESS_ERROR, string.Empty);
        }
    }
}
