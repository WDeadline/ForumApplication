using ForumApi.Interfaces;
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
    public class ObjectiveRepository : IRepository<Objective>
    {
        private readonly ILogger<ObjectiveRepository> _logger;
        private readonly IForumDbConnector _db;

        public ObjectiveRepository(ILogger<ObjectiveRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Objective entity)
        {
            try
            {
                return _db.Objectives.InsertOneAsync(entity);
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
                FilterDefinition<Objective> filter = Builders<Objective>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Objectives.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Objective, bool>> where)
        {
            try
            {
                FilterDefinition<Objective> filter = Builders<Objective>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Objectives.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Objective> Get(Expression<Func<Objective, bool>> where)
        {
            try
            {
                FilterDefinition<Objective> filter = Builders<Objective>.Filter.Where(where);
                return await _db.Objectives.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Objective>> GetAll()
        {
            try
            {
                return await _db.Objectives.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Objective> GetById(string id)
        {
            try
            {
                FilterDefinition<Objective> filter = Builders<Objective>.Filter.Eq(u => u.Id, id);
                return await _db.Objectives.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Objective>> GetMany(Expression<Func<Objective, bool>> where)
        {
            try
            {
                FilterDefinition<Objective> filter = Builders<Objective>.Filter.Where(where);
                return await _db.Objectives.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Objective entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Objectives
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
