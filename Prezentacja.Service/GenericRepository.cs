using System.Collections.Generic;
using System.Linq;
using Prezentacja.Common;
using Prezentacja.DTO;

namespace Prezentacja.Service
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        private Dictionary<int, IEntity> items = DatabaseContext.Items;

        public List<T> GetAll()
        {
            return items
                .Where(i => { return i.Value.GetType() == typeof(T); })
                .Select(i => i.Value)
                .Cast<T>()
                .ToList();
        }

        public T GetById(int id)
        {
            return items[id] as T;
        }

        public int Count()
        {
            return GetAll().Count;
        }

        public void Add(T entity)
        {
            int newId = items.Max(i => i.Key) + 1;

            items.Add(newId, entity);

            entity.Id = newId;
        }

        public void Update(T entity)
        {
            if (entity.Id > 0)
                items[entity.Id] = entity;
        }

        public void DeleteById(int id)
        {
            items.Remove(id);
        }

        public void DeleteAll()
        {
            var itemsToRemove = GetAll();
            itemsToRemove.ForEach(i => items.Remove(i.Id));
        }
    }
}
