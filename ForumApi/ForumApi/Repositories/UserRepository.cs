using ForumApi.Interfaces.Contexts;
using ForumApi.Interfaces.Repositories;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ForumApi.Repositories
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

        public Task AddAsync(User entity)
        {
            try
            {
                return _db.Users.InsertOneAsync(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, entity);
                throw e;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Users.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, id);
                throw e;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> where)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Users.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _db.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> where)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where(where);
                return await _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, id);
                return await _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, id);
                throw e;
            }

            
        }

        public async Task<IEnumerable<User>> GetManyAsync(Expression<Func<User, bool>> where)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where(where);
                return await _db.Users.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public Task<User> GetUserByEmailAddressAsync(string emailAddress)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.EmailAddress, emailAddress);
                return _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, emailAddress);
                throw e;
            }
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Username, username);
                return _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, username);
                throw e;
            }
        }

        public Task<User> GetUserByUsernameOrEmailAddressAsync(string usernameOrEmailAddress)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.And(
                    Builders<User>.Filter.Or(
                        Builders<User>.Filter.Eq(u => u.Username, usernameOrEmailAddress),
                        Builders<User>.Filter.Eq(u => u.EmailAddress, usernameOrEmailAddress)
                    ),
                    Builders<User>.Filter.Eq(u => u.Active, true)
                );
                return _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, usernameOrEmailAddress);
                throw e;
            }
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Users
                    .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, entity);
                throw e;
            }
        }
    }
}
