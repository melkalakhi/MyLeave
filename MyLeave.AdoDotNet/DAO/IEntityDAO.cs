using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLeave.AdoDotNet.DAO
{
    public interface IEntityDAO<T> where T : DTO.EntityDTO
    {
        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        T GetById(int id);

        IList<T> GetAll();

        IList<T> Find(Func<T, bool> predicate);

        int Count();

        int Count(Func<T, bool> predicate);
    }
}
