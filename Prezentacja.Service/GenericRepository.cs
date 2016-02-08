using Prezentacja.Common;
using Prezentacja.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Prezentacja.Service
{
    public class DatabaseContext
    {
        private static Dictionary<int, IEntity> _items = new Dictionary<int, IEntity>()
        {
            {1, new Person { Id = 1, Name = "Mikolaj", Surname = "Juskiewicz", Age = 25, PhoneNumber = "+48505050788"} },
            {2, new Person { Id = 2, Name = "Tomasz", Surname = "Juskiewicz", Age = 25, PhoneNumber = "+48505050789"} },
            {3, new Message { Id = 3, PhoneNumber = "+48505050788", Text = "Hello world!!!"} },
            {4, new Message { Id = 4, PhoneNumber = "+48505050788", Text = "Hello world!!!!"} },
        };

        public static Dictionary<int, IEntity> Items
        {
            get
            {
                return _items;
            }
        }
    }

    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        private Dictionary<int, IEntity> Items = DatabaseContext.Items;

        public List<T> GetAll()
        {
            var foo = Items
                .Where(i => { return i.Value.GetType() == typeof(T); })
                .Select(i => i.Value)
                .Cast<T>()
                .ToList();
            return foo;
        }

        public T GetById(int id)
        {
            return Items[id] as T;
        }

        public int Count()
        {
            return GetAll().Count;
        }

        public void Add(T entity)
        {
            int newId = Items.Max(i => i.Key) + 1;

            Items.Add(newId, entity);

            entity.Id = newId;
        }

        public void Update(T entity)
        {
            if (entity.Id > 0)
                Items[entity.Id] = entity;
        }

        public void DeleteById(int id)
        {
            Items.Remove(id);
        }

        public void DeleteAll()
        {
            var itemsToRemove = GetAll();
            itemsToRemove.ForEach(i => Items.Remove(i.Id));
        }
    }
}
