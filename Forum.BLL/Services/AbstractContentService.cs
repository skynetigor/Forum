using Forum.BLL.Interfaces;
using System;
using System.Collections.Generic;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.DTO.Content;

namespace Forum.BLL.Services
{
    public abstract class AbstractContentService<TContent> : IContentService<TContent> where TContent : BaseEntityDTO
    {
        protected const string ACCESS_ERROR = "У вас нет прав на данное действие!";
        public abstract TContent FindById(int id);

        public abstract IEnumerable<TContent> Get();

        protected abstract OperationDetails CreateContent(UserDTO user, TContent category);

        protected abstract OperationDetails UpdateContent(UserDTO user, TContent category);

        protected abstract OperationDetails DeleteContent(UserDTO user, TContent category);

        public OperationDetails Create(UserDTO user, TContent category)
        {
            try
            {
                return CreateContent(user, category);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }

        public OperationDetails Update(UserDTO user, TContent category)
        {
            try
            {
                return UpdateContent(user, category);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }

        public OperationDetails Delete(UserDTO user, TContent category)
        {
            try
            {
                return DeleteContent(user, category);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }
        }
    }
}
