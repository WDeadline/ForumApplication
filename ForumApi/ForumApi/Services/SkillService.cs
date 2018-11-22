using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class SkillService
    {
        private readonly ILogger<SkillService> _logger;
        private readonly IRepository<Skill> _skillRepository;

        public SkillService(ILogger<SkillService> logger, IRepository<Skill> skillRepository)
        {
            _logger = logger;
            _skillRepository = skillRepository;

        }

        public Task Add(Skill entity)
        {
            try
            {
                return _skillRepository.Add(entity);
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
                return _skillRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Skill>> GetAll()
        {
            try
            {
                return _skillRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<Skill> GetById(string id)
        {
            try
            {
                return _skillRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Skill>> GetByUserId(string userId)
        {
            try
            {
                return _skillRepository.GetMany(e => e.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, userId);
                throw ex;
            }
        }

        public Task<bool> Update(Skill entity)
        {
            try
            {
                entity.UpdationTime = DateTime.UtcNow;
                return _skillRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
