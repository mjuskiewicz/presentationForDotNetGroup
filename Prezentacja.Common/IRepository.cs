using System.Collections.Generic;

namespace Prezentacja.Common
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> GetAll();

        T GetById(int id);

        int Count();

        void Add(T entity);

        void Update(T entity);

        void DeleteById(int id);

        void DeleteAll();
    }
}
