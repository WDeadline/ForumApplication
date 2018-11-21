using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class ExperienceService : IService<Experience>
    {
        private readonly ILogger<ExperienceService> _logger;
        private readonly IRepository<Experience> _experienceRepository;

        public ExperienceService(ILogger<ExperienceService> logger, IRepository<Experience> experienceRepository)
        {
            _logger = logger;
            _experienceRepository = experienceRepository;

        }

        public Task Add(Experience entity)
        {
            try
            {
                return _experienceRepository.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                return _experienceRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Experience>> GetAll()
        {
            try
            {
                return _experienceRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<Experience> GetById(string id)
        {
            try
            {
                return _experienceRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Experience>> GetByUserId(string userId)
        {
            try
            {
                return _experienceRepository.GetMany(e => e.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, userId);
                throw ex;
            }
        }

        public Task<bool> Update(Experience entity)
        {
            try
            {
                entity.UpdationTime = DateTime.UtcNow;
                return _experienceRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
