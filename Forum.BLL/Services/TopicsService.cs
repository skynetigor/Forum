using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.DTO.Content;
using Forum.DAL.Interfaces;
using Forum.DAL.Entities.Topics;
using Forum.DAL.Entities.Categories;
using Forum.BLL.DTO.Content.Category;
using Microsoft.AspNet.Identity;
using Forum.DAL.Entities;
using Forum.BLL.Interfaces;

namespace Forum.BLL.Services
{
    public class TopicsService : AbstractContentService<TopicDTO>, ITopicService
    {
        private IUnitOfWork identity;
        private IGenericRepository<Topic> topicRepository;
        private IGenericRepository<SubCategory> subCategoryRepository;
        public TopicsService(IUnitOfWork identity, IGenericRepository<Topic> topicRepository, IGenericRepository<SubCategory> subCategoryRepository)
        {
            this.identity = identity;
            this.topicRepository = topicRepository;
            this.subCategoryRepository = subCategoryRepository;
        }
        public override TopicDTO FindById(int id)
        {
            var topic = topicRepository.FindById(id);
            return Extract(topic);
        }

        public override IEnumerable<TopicDTO> Get()
        {
            var topics = topicRepository.Get();
            var list = new List<TopicDTO>();
            foreach(var t in topics)
            {
                list.Add(Extract(t));
            }
            return list;
        }

        protected override OperationDetails CreateContent(UserDTO user, TopicDTO topic)
        {
            var appuser = identity.UserManager.Users.First(u => u.Id == user.Id);
            if(!appuser.IsBlocked)
            {
                var subcat = subCategoryRepository.FindById(topic.SubCategoryId);
                var apptopic = new Topic()
                {
                    Message = topic.Message,
                    Title = topic.Title,
                    User = appuser
                };
                var comment = new Comment
                {
                    Date = DateTime.Now,
                    Message = topic.Message,
                    User = appuser
                };
                apptopic.Comments.Add(comment);
                subcat.Topics.Add(apptopic);
                subCategoryRepository.Update(subcat);
                return new OperationDetails(true, string.Empty);
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails UpdateContent(UserDTO user, TopicDTO topic)
        {
            var appuser = identity.UserManager.Users.First(u => u.Id == user.Id);
            if(appuser.Id == user.Id && !appuser.IsBlocked)
            {
                var apptopic = topicRepository.FindById(topic.Id);
                apptopic.Message = topic.Message;
                apptopic.Title = topic.Message;
                topicRepository.Update(apptopic);
                return new OperationDetails(true, string.Empty);
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        protected override OperationDetails DeleteContent(UserDTO user, TopicDTO topic)
        {
            var appuser = identity.UserManager.Users.First(u => u.Id == user.Id);
            if (appuser.Id == user.Id && !appuser.IsBlocked)
            {
                var apptopic = topicRepository.FindById(topic.Id);
                topicRepository.Remove(apptopic);
                return new OperationDetails(true, string.Empty);
            }
            return new OperationDetails(false, ACCESS_ERROR);
        }

        private TopicDTO Extract(Topic topic)
        {
            var topicDTO = new TopicDTO
            {
                Id = topic.Id,
                IsBlocked = topic.IsBlocked,
                Title = topic.Title,
                SubCategoryName = topic.SubCategory.Name,
                Message = topic.Message,
                UserName = topic.User.UserName,
                AnswersCount = topic.Comments.Count()
            };
            return topicDTO;
        }

        public IEnumerable<TopicDTO> GetTopicsBySubCategoryId(int id)
        {
            var topics = subCategoryRepository.FindById(id).Topics;
            List<TopicDTO> tdoList = new List<TopicDTO>();
            foreach(var t in topics)
            {
                var topicDTO = Extract(t);
                tdoList.Add(topicDTO);
            }
            return tdoList;
        }
    }
}
