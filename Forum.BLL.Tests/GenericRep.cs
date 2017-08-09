using Forum.DAL.Entities.Categories;
using Forum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Tests
{
    class GenericRep<TEntity> : IGenericRepository<TEntity> where TEntity : AbstractCategory
    {
        List<TEntity> list = new List<TEntity>();
        int i = 0;
        int index { get { return i++; } }
        public void Create(TEntity item)
        {
            item.Id = index;
            list.Add(item);
        }

        public TEntity FindById(int id)
        {
            return list.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TEntity> Get()
        {
            return list;
        }

        public void Remove(TEntity item)
        {
            list.Remove(item);
        }

        public void Update(TEntity item)
        {
            TEntity entity = list.FirstOrDefault(t=> t.Id == item.Id);
            entity.Name = item.Name;
            entity.Title = item.Title;
        }
    }
}
