using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ForumApi.Interfaces.Services
{
    public interface IService<T>
    {
        Task AddAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> where);
        Task<bool> UpdateAsync(T entity);
        Task<T> GetByIdAsync(string id);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);
    }
}
