using ForumApi.Environments;
using ForumApi.Helpers;
using ForumApi.Interfaces;
using ForumApi.Interfaces.Repositories;
using ForumApi.Models;
using ForumApi.Payloads;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly JwtTokenSettings jwtTokenSettings;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IOptions<JwtTokenSettings> options,
            IUserRepository userRepository)
        {
            _logger = logger;
            jwtTokenSettings = options.Value;
            _userRepository = userRepository;

        }

        public Task Add(User entity)
        {
            try
            {
                return _userRepository.Add(entity);
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
                return _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<User> GetById(string id)
        {
            try
            {
                return _userRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<bool> Update(User entity)
        {
            try
            {
                return _userRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
