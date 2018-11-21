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
    public class ExperienceRepository : IRepository<Experience>
    {
        private readonly ILogger<ExperienceRepository> _logger;
        private readonly IForumDbConnector _db;

        public ExperienceRepository(ILogger<ExperienceRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Experience entity)
        {
            try
            {
                return _db.Experiences.InsertOneAsync(entity);
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
                FilterDefinition<Experience> filter = Builders<Experience>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Experiences.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Experience, bool>> where)
        {
            try
            {
                FilterDefinition<Experience> filter = Builders<Experience>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Experiences.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Experience> Get(Expression<Func<Experience, bool>> where)
        {
            try
            {
                FilterDefinition<Experience> filter = Builders<Experience>.Filter.Where(where);
                return await _db.Experiences.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Experience>> GetAll()
        {
            try
            {
                return await _db.Experiences.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Experience> GetById(string id)
        {
            try
            {
                FilterDefinition<Experience> filter = Builders<Experience>.Filter.Eq(u => u.Id, id);
                return await _db.Experiences.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Experience>> GetMany(Expression<Func<Experience, bool>> where)
        {
            try
            {
                FilterDefinition<Experience> filter = Builders<Experience>.Filter.Where(where);
                return await _db.Experiences.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Experience entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Experiences
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
