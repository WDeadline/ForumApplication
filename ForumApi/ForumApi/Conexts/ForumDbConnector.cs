using ForumApi.Environments;
using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Contexts
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

        public IMongoCollection<Question> Questions => _db.GetCollection<Question>("Questions");

        public IMongoCollection<Work> Works => _db.GetCollection<Work>("Works");

        public IMongoCollection<Recruitment> Recruitments => _db.GetCollection<Recruitment>("Recruitments");

        public IMongoCollection<Interview> Interviews => _db.GetCollection<Interview>("Interviews");
    }
}
