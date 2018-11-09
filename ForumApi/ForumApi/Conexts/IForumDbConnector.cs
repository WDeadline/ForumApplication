using ForumApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Contexts
{
    public interface IForumDbConnector
    {
        IMongoCollection<User> Users { get; }
    }
}
