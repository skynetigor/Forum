using Forum.DAL.EF;
using Forum.DAL.Entities;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Forum.DAL.Repositories
{
    public class Repository<TEntity> : IGenericRepository<TEntity> where TEntity :class
    {
        private ApplicationContext context;
        private DbSet<TEntity> dbset;
        public Repository(ApplicationContext context)
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
