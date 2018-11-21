using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task<bool> Delete(string id);
        Task<bool> Delete(Expression<Func<T, bool>> where);
        Task<bool> Update(T entity);
        Task<T> Get(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where);
    }
}
