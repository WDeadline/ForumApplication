using ForumApi.Contexts;
using ForumApi.Models;
using ForumApi.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.SourceCode.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IForumDbConnector _db;

        public UserRepository(ILogger<UserRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task Create(User obj) => await _db.Users.InsertOneAsync(obj);

        public async Task<bool> Delete(ObjectId id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, id);
            DeleteResult deleteResult = await _db.Users.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<User>> Get()
        {
            try
            {
                return await _db.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<User> Get(ObjectId id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return _db.Users.Find(filter).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByEmailAddress(string emailAddress)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.EmailAddress, emailAddress);
            return _db.Users.Find(filter).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUsername(string username)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return _db.Users.Find(filter).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUsernameOrEmailAddress(string usernameOrEmailAddress)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Or(
                    Builders<User>.Filter.Eq(u => u.Username, usernameOrEmailAddress),
                    Builders<User>.Filter.Eq(u => u.EmailAddress, usernameOrEmailAddress)
                );
                return _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Update(User obj)
        {
            ReplaceOneResult updateResult = 
                await _db.Users.ReplaceOneAsync(filter: g => g.Id == obj.Id,replacement: obj);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
