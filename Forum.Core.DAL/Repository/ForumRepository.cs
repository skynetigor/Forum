using Forum.Core.DAL.Interfaces;
using Forum.NewDAL.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Forum.NewDAL.Repository
{
    public class ForumRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext context;
        private DbSet<TEntity> dbset;
        public ForumRepository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            dbset.Add(item);
            context.SaveChanges();
        }

        public TEntity FindById(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return dbset.ToArray();
        }

        public void Remove(TEntity item)
        {
            dbset.Remove(item);
            context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
