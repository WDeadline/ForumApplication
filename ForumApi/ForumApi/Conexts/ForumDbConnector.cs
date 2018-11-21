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

        public IMongoCollection<Education> Educations => _db.GetCollection<Education>("Educations");

        public IMongoCollection<Experience> Experiences => _db.GetCollection<Experience>("Experiences");

        public IMongoCollection<Objective> Objectives => _db.GetCollection<Objective>("Objectives");

        public IMongoCollection<Information> Informations => _db.GetCollection<Information>("Informations");
    }
}
