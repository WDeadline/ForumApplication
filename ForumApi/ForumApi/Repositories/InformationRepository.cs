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
    public class InformationRepository : IRepository<Information>
    {
        private readonly ILogger<InformationRepository> _logger;
        private readonly IForumDbConnector _db;

        public InformationRepository(ILogger<InformationRepository> logger, IForumDbConnector db)
        {
            _logger = logger;
            _db = db;
        }

        public Task Add(Information entity)
        {
            try
            {
                return _db.Informations.InsertOneAsync(entity);
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
                FilterDefinition<Information> filter = Builders<Information>.Filter.Eq(u => u.Id, id);
                DeleteResult deleteResult = await _db.Informations.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<bool> Delete(Expression<Func<Information, bool>> where)
        {
            try
            {
                FilterDefinition<Information> filter = Builders<Information>.Filter.Where(where);
                DeleteResult deleteResult = await _db.Informations.DeleteManyAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Information> Get(Expression<Func<Information, bool>> where)
        {
            try
            {
                FilterDefinition<Information> filter = Builders<Information>.Filter.Where(where);
                return await _db.Informations.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Information>> GetAll()
        {
            try
            {
                return await _db.Informations.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<Information> GetById(string id)
        {
            try
            {
                FilterDefinition<Information> filter = Builders<Information>.Filter.Eq(u => u.Id, id);
                return await _db.Informations.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public async Task<IEnumerable<Information>> GetMany(Expression<Func<Information, bool>> where)
        {
            try
            {
                FilterDefinition<Information> filter = Builders<Information>.Filter.Where(where);
                return await _db.Informations.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> Update(Information entity)
        {
            try
            {
                ReplaceOneResult updateResult = await _db.Informations
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
