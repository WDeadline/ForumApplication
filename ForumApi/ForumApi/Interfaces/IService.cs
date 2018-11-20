using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IService<T> where T : class
    {
        Task Add(T entity);
        Task<bool> Delete(string id);
        Task<bool> Update(T entity);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
    }
}
