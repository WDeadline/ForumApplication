using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class EducationService : IService<Education>
    {
        private readonly ILogger<EducationService> _logger;
        private readonly IRepository<Education> _educationRepository;

        public EducationService(ILogger<EducationService> logger, IRepository<Education> educationRepository)
        {
            _logger = logger;
            _educationRepository = educationRepository;

        }

        public Task Add(Education entity)
        {
            try
            {
                return _educationRepository.Add(entity);
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
                return _educationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Education>> GetAll()
        {
            try
            {
                return _educationRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<Education> GetById(string id)
        {
            try
            {
                return _educationRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<bool> Update(Education entity)
        {
            try
            {
                return _educationRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
