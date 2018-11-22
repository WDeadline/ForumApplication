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
    public class SkillRepository
    {
        private readonly ILogger<SkillRepository> _logger;
        private readonly IForumDbConnector _db;

        public SkillRepository(ILogger<SkillRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Skill entity)
        {
            try
            {
                return _db.Skills.InsertOneAsync(entity);
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
                FilterDefinition<Skill> filter = Builders<Skill>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Skills.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Skill, bool>> where)
        {
            try
            {
                FilterDefinition<Skill> filter = Builders<Skill>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Skills.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Skill> Get(Expression<Func<Skill, bool>> where)
        {
            try
            {
                FilterDefinition<Skill> filter = Builders<Skill>.Filter.Where(where);
                return await _db.Skills.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Skill>> GetAll()
        {
            try
            {
                return await _db.Skills.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Skill> GetById(string id)
        {
            try
            {
                FilterDefinition<Skill> filter = Builders<Skill>.Filter.Eq(u => u.Id, id);
                return await _db.Skills.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Skill>> GetMany(Expression<Func<Skill, bool>> where)
        {
            try
            {
                FilterDefinition<Skill> filter = Builders<Skill>.Filter.Where(where);
                return await _db.Skills.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Skill entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Skills
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
