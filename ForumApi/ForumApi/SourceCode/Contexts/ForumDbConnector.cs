using ForumApi.Contexts;
using ForumApi.Environment;
using ForumApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.SourceCode.Contexts
{
    public class ForumDbConnector : IForumDbConnector
    {
        private readonly IMongoDatabase _db;

        public ForumDbConnector(IOptions<ConnectionSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>("Users");
    }
}
