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
    public class UserRepository : IRepository<User>
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IForumDbConnector _db;

        public UserRepository(ILogger<UserRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(User entity)
        {
            try
            {
                return _db.Users.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Users.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<User, bool>> where)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Users.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<User> Get(Expression<Func<User, bool>> where)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where(where);
                return await _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _db.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, id);
                return await _db.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> GetMany(Expression<Func<User, bool>> where)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Where(where);
                return await _db.Users.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(User entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Users
                    .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
