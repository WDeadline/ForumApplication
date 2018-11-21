using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class InformationService : IService<Information>
    {
        private readonly ILogger<InformationService> _logger;
        private readonly IRepository<Information> _informationRepository;

        public InformationService(ILogger<InformationService> logger, IRepository<Information> informationRepository)
        {
            _logger = logger;
            _informationRepository = informationRepository;

        }

        public Task Add(Information entity)
        {
            try
            {
                return _informationRepository.Add(entity);
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
                return _informationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Information>> GetAll()
        {
            try
            {
                return _informationRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<Information> GetById(string id)
        {
            try
            {
                return _informationRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<Information> GetByUserId(string userId)
        {
            try
            {
                return _informationRepository.Get(e => e.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, userId);
                throw ex;
            }
        }

        public Task<bool> Update(Information entity)
        {
            try
            {
                entity.UpdationTime = DateTime.UtcNow;
                return _informationRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
