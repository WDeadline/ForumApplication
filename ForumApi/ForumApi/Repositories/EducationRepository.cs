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
    public class EducationRepository : IRepository<Education>
    {

        private readonly ILogger<EducationRepository> _logger;
        private readonly IForumDbConnector _db;

        public EducationRepository(ILogger<EducationRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Education entity)
        {
            try
            {
                return _db.Educations.InsertOneAsync(entity);
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
                FilterDefinition<Education> filter = Builders<Education>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Educations.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Education, bool>> where)
        {
            try
            {
                FilterDefinition<Education> filter = Builders<Education>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Educations.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Education> Get(Expression<Func<Education, bool>> where)
        {
            try
            {
                FilterDefinition<Education> filter = Builders<Education>.Filter.Where(where);
                return await _db.Educations.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Education>> GetAll()
        {
            try
            {
                return await _db.Educations.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Education> GetById(string id)
        {
            try
            {
                FilterDefinition<Education> filter = Builders<Education>.Filter.Eq(u => u.Id, id);
                return await _db.Educations.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Education>> GetMany(Expression<Func<Education, bool>> where)
        {
            try
            {
                FilterDefinition<Education> filter = Builders<Education>.Filter.Where(where);
                return await _db.Educations.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Education entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Educations
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
