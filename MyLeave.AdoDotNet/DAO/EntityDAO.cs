using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLeave.AdoDotNet.DAO
{
    public abstract class EntityDAO<T> : IEntityDAO<T> where T : DTO.EntityDTO
    {
        /// <summary>
        /// La chaine de connexion
        /// </summary>
        protected string connectionString = @"data source=localhost\sqlexpress;initial catalog=LeaveManagement;user id=sa;password=sasa2015;";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Add(T entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Delete(T entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Update(T entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IList<T> GetAll();

        /// <summary>
        /// Recherche selon un prédicat
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IList<T> Find(Func<T, bool> predicate)
        {
            try
            {
                IList<T> entities = GetAll();
                IList<T> result = entities.Where(predicate).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            try
            {
                IList<T> entities = GetAll();
                int count = entities != null ? entities.Count() : 0;
                return count;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(Func<T, bool> predicate)
        {
            try
            {
                IList<T> entities = GetAll();
                int count = entities != null ? entities.Count(predicate) : 0;
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
