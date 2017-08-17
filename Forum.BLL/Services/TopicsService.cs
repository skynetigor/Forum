using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Core.DAL.Entities.Content;
using Forum.NewBLL.Services;
using Forum.Core.DAL.Interfaces;
using Forum.Core.DAL.Entities.Content.Categories;
using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Identity;

namespace Forum.BLL.Services
{
    public class TopicsService : AbstractContentService<Topic>
    {
        private IIdentityProvider identity;
        private IGenericRepository<Topic> topicRepository;
        private IGenericRepository<SubCategory> subCategoryRepository;
        public TopicsService(IIdentityProvider identity, IGenericRepository<Topic> topicRepository, IGenericRepository<SubCategory> subCategoryRepository)
        {
            this.identity = identity;
            this.topicRepository = topicRepository;
            this.subCategoryRepository = subCategoryRepository;
        }
        public override Topic FindById(int id)
        {
            return topicRepository.FindById(id);
        }

        public override IEnumerable<Topic> Get()
        {
            return topicRepository.Get();
        }

        protected override OperationDetails CreateContent(AppUser user, Topic topic)//
        {

            var appuser = identity.UserManager.Users.First(u => u.Id == topic.User.Id);
            var subcat = subCategoryRepository.FindById(topic.SubCategory.Id);
            topic.Id = 0;
            topic.User = appuser;
            topic.SubCategory = null;
            var comment = new Comment
            {
                Date = DateTime.Now,
                Message = topic.Message,
                User = appuser
            };
            topic.Comments.Add(comment);
            subcat.Topics.Add(topic);

            subCategoryRepository.Update(subcat);
            return new OperationDetails(true, string.Empty);
        }

        protected override OperationDetails UpdateContent(AppUser user, Topic topic)//
        {
            var apptopic = topicRepository.FindById(topic.Id);
            apptopic.Message = topic.Message;
            apptopic.Description = topic.Message;
            topicRepository.Update(apptopic);
            return new OperationDetails(true, string.Empty);
        }

        protected override OperationDetails DeleteContent(AppUser user, Topic topic)//
        {
            var appuser = identity.UserManager.Users.First(u => u.Id == topic.User.Id);

            var apptopic = topicRepository.FindById(topic.Id);
            topicRepository.Remove(apptopic);
            return new OperationDetails(true, string.Empty);
        }
    }
}
