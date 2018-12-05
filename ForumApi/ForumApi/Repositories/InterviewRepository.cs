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
    public class InterviewRepository : IRepository<Interview>
    {
        private readonly ILogger<InterviewRepository> _logger;
        private readonly IForumDbConnector _db;

        public InterviewRepository(ILogger<InterviewRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Interview entity)
        {
            try
            {
                return _db.Interviews.InsertOneAsync(entity);
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
                FilterDefinition<Interview> filter = Builders<Interview>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Interviews.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Interview, bool>> where)
        {
            try
            {
                FilterDefinition<Interview> filter = Builders<Interview>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Interviews.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Interview> Get(Expression<Func<Interview, bool>> where)
        {
            try
            {
                FilterDefinition<Interview> filter = Builders<Interview>.Filter.Where(where);
                return await _db.Interviews.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Interview>> GetAll()
        {
            try
            {
                return await _db.Interviews.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Interview> GetById(string id)
        {
            try
            {
                FilterDefinition<Interview> filter = Builders<Interview>.Filter.Eq(u => u.Id, id);
                return await _db.Interviews.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Interview>> GetMany(Expression<Func<Interview, bool>> where)
        {
            try
            {
                FilterDefinition<Interview> filter = Builders<Interview>.Filter.Where(where);
                return await _db.Interviews.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Interview entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Interviews
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
