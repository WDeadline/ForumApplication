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
    public class QuestionRepository : IRepository<Question>
    {
        private readonly ILogger<QuestionRepository> _logger;
        private readonly IForumDbConnector _db;

        public QuestionRepository(ILogger<QuestionRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Question entity)
        {
            try
            {
                return _db.Questions.InsertOneAsync(entity);
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
                FilterDefinition<Question> filter = Builders<Question>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Questions.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Question, bool>> where)
        {
            try
            {
                FilterDefinition<Question> filter = Builders<Question>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Questions.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Question> Get(Expression<Func<Question, bool>> where)
        {
            try
            {
                FilterDefinition<Question> filter = Builders<Question>.Filter.Where(where);
                return await _db.Questions.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            try
            {
                return await _db.Questions.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Question> GetById(string id)
        {
            try
            {
                FilterDefinition<Question> filter = Builders<Question>.Filter.Eq(u => u.Id, id);
                return await _db.Questions.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Question>> GetMany(Expression<Func<Question, bool>> where)
        {
            try
            {
                FilterDefinition<Question> filter = Builders<Question>.Filter.Where(where);
                return await _db.Questions.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Question entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Questions
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
