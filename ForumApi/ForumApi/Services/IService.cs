using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(string id);
        Task Create(T obj);
        Task<bool> Update(T obj);
        Task<bool> Delete(string id);
    }
}
