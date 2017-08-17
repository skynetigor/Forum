using System.Collections.Generic;
using System.Linq;
using System;
using Forum.Core.DAL.Entities.Content;
using Forum.Core.BLL.Interfaces;
using Forum.Core.DAL.Interfaces;
using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace Forum.NewBLL.Services
{
    public class CommentService : AbstractContentService<Comment>
    {
        private IGenericRepository<Comment> commentRepository;
        private IGenericRepository<Topic> topicRepository;
        private IIdentityProvider identity;

        public CommentService(IGenericRepository<Comment> commentRepository, IGenericRepository<Topic> topicRepository, IIdentityProvider identity)
        {
            this.commentRepository = commentRepository;
            this.topicRepository = topicRepository;
            this.identity = identity;
        }

        public override IEnumerable<Comment> Get()
        {
            return commentRepository.Get();
        }

        public override Comment FindById(int id)
        {
            return commentRepository.FindById(id);
        }

        protected override OperationDetails CreateContent(AppUser user, Comment comment)
        {
            var appuser = identity.UserManager.FindById(comment.User.Id);
            var topic = topicRepository.FindById(comment.Topic.Id);
            comment.Date = DateTime.Now;
            comment.Topic = null;
            comment.User = appuser;
            topic.Comments.Add(comment);
            topicRepository.Update(topic);
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails UpdateContent(AppUser user, Comment comment)
        {
            var appuser = identity.UserManager.Users.First();
            var topic = topicRepository.FindById(comment.Topic.Id);
            var com = commentRepository.FindById(comment.Id);
            com.Date = comment.Date;
            com.Message = comment.Message;
            com.Topic = topic;
            commentRepository.Update(com);
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails DeleteContent(AppUser user, Comment comment)
        {
            var appuser = identity.UserManager.Users.First();
            var topic = topicRepository.FindById(comment.Topic.Id);
            return new OperationDetails(false, ACCESS_ERROR);
        }
    }
}
