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
    public class WorkRepository : IRepository<Work>
    {
        private readonly ILogger<WorkRepository> _logger;
        private readonly IForumDbConnector _db;

        public WorkRepository(ILogger<WorkRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Work entity)
        {
            try
            {
                return _db.Works.InsertOneAsync(entity);
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
                FilterDefinition<Work> filter = Builders<Work>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Works.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Work, bool>> where)
        {
            try
            {
                FilterDefinition<Work> filter = Builders<Work>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Works.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Work> Get(Expression<Func<Work, bool>> where)
        {
            try
            {
                FilterDefinition<Work> filter = Builders<Work>.Filter.Where(where);
                return await _db.Works.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Work>> GetAll()
        {
            try
            {
                return await _db.Works.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Work> GetById(string id)
        {
            try
            {
                FilterDefinition<Work> filter = Builders<Work>.Filter.Eq(u => u.Id, id);
                return await _db.Works.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Work>> GetMany(Expression<Func<Work, bool>> where)
        {
            try
            {
                FilterDefinition<Work> filter = Builders<Work>.Filter.Where(where);
                return await _db.Works.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Work entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Works
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
