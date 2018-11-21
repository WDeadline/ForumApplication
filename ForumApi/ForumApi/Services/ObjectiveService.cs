using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class ObjectiveService : IService<Objective>
    {
        private readonly ILogger<ObjectiveService> _logger;
        private readonly IRepository<Objective> _objectiveRepository;

        public ObjectiveService(ILogger<ObjectiveService> logger, IRepository<Objective> objectiveRepository)
        {
            _logger = logger;
            _objectiveRepository = objectiveRepository;

        }

        public Task Add(Objective entity)
        {
            try
            {
                return _objectiveRepository.Add(entity);
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
                return _objectiveRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Objective>> GetAll()
        {
            try
            {
                return _objectiveRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<Objective> GetById(string id)
        {
            try
            {
                return _objectiveRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<bool> Update(Objective entity)
        {
            try
            {
                return _objectiveRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
