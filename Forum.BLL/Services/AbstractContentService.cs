using System;
using System.Collections.Generic;
using Forum.Core.BLL.Interfaces;
using Forum.Core.BLL.Infrastructure;
using Forum.Core.DAL.Entities.Identity;

namespace Forum.NewBLL.Services
{
    public abstract class AbstractContentService<TContent> : IContentService<TContent> where TContent : class
    {
        protected const string ACCESS_ERROR = "У вас нет прав на данное действие!";
        protected const string ADMIN_ROLE = "admin";
        public abstract TContent FindById(int id);

        public abstract IEnumerable<TContent> Get();

        protected abstract OperationDetails CreateContent(AppUser user, TContent category);

        protected abstract OperationDetails UpdateContent(AppUser user, TContent category);

        protected abstract OperationDetails DeleteContent(AppUser user, TContent category);

        public OperationDetails Create(AppUser user, TContent category)
        {
            try
            {
                return CreateContent(user, category);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message);
            }
        }

        public OperationDetails Update(AppUser user, TContent category)
        {
            try
            {
                return UpdateContent(user, category);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message);
            }
        }

        public OperationDetails Delete(AppUser user, TContent category)
        {
            try
            {
                return DeleteContent(user, category);
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message);
            }
        }
    }
}
