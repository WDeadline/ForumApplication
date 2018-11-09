using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(ObjectId id);
        Task Create(T obj);
        Task<bool> Update(T obj);
        Task<bool> Delete(ObjectId id);
    }
}
