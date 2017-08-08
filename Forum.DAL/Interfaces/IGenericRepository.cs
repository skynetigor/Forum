using System.Collections.Generic;

namespace Forum.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
