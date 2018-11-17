using ForumApi.Interfaces.Repositories;
using ForumApi.Interfaces.Services;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository) {
            _logger = logger;
            _userRepository = userRepository;
        }

        public Task AddAsync(User entity)
        {
            try
            {
                return _userRepository.AddAsync(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, entity);
                throw e;
            }
        }

        public Task<bool> DeleteAsync(string id)
        {
            try
            {
                return _userRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, id);
                throw e;
            }
        }

        public Task<bool> DeleteAsync(Expression<Func<User, bool>> where)
        {
            try
            {
                return _userRepository.DeleteAsync(where);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return _userRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public Task<User> GetAsync(Expression<Func<User, bool>> where)
        {
            try
            {
                return _userRepository.GetAsync(where);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public Task<User> GetByIdAsync(string id)
        {
            try
            {
                return _userRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, id);
                throw e;
            }
        }

        public Task<IEnumerable<User>> GetManyAsync(Expression<Func<User, bool>> where)
        {
            try
            {
                return _userRepository.GetManyAsync(where);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }

        public Task<bool> UpdateAsync(User entity)
        {
            try
            {
                return _userRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, entity);
                throw e;
            }
        }
    }
}
