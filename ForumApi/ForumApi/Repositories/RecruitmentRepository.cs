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
    public class RecruitmentRepository : IRepository<Recruitment>
    {
        private readonly ILogger<RecruitmentRepository> _logger;
        private readonly IForumDbConnector _db;

        public RecruitmentRepository(ILogger<RecruitmentRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Recruitment entity)
        {
            try
            {
                return _db.Recruitments.InsertOneAsync(entity);
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
                FilterDefinition<Recruitment> filter = Builders<Recruitment>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Recruitments.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Recruitment, bool>> where)
        {
            try
            {
                FilterDefinition<Recruitment> filter = Builders<Recruitment>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Recruitments.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Recruitment> Get(Expression<Func<Recruitment, bool>> where)
        {
            try
            {
                FilterDefinition<Recruitment> filter = Builders<Recruitment>.Filter.Where(where);
                return await _db.Recruitments.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Recruitment>> GetAll()
        {
            try
            {
                return await _db.Recruitments.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Recruitment> GetById(string id)
        {
            try
            {
                FilterDefinition<Recruitment> filter = Builders<Recruitment>.Filter.Eq(u => u.Id, id);
                return await _db.Recruitments.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Recruitment>> GetMany(Expression<Func<Recruitment, bool>> where)
        {
            try
            {
                FilterDefinition<Recruitment> filter = Builders<Recruitment>.Filter.Where(where);
                return await _db.Recruitments.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Recruitment entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Recruitments
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
