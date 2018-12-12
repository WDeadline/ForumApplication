using ForumApi.Environments;
using ForumApi.Helpers;
using ForumApi.Interfaces;
using ForumApi.Models;
using ForumApi.Payloads;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;

        }

        public Task Add(User user)
        {
            try
            {
                return _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, user);
                throw ex;
            }
        }

        public async Task<User> CreateUser(CreationUser creationUser)
        {
            PasswordManager.CreatePasswordHash(creationUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User {
                Name = creationUser.Name,
                Birthday = creationUser.Birthday,
                PhoneNumber = creationUser.PhoneNumber,
                Address = creationUser.Address,
                Position = creationUser.Position,
                Username = creationUser.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailAddress = creationUser.EmailAddress,
                Roles = creationUser.Roles,
            };
            await _userRepository.Add(user);
            return user;
        }

        public async Task<bool> IsExistedEmailAddressAsync(string emailAddress)
        {
            var user = await _userRepository.Get(u => u.EmailAddress == emailAddress);
            return (user != null);
        }

        public async Task<bool> IsExistedUsernameAsync(string username)
        {
            var user = await _userRepository.Get(u => u.Username == username);
            return (user != null);
        }

        public bool RegisterAsync(Register register)
        {
            try
            {
                PasswordManager.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User
                {
                    Name = register.Name,
                    Username = register.Username,
                    EmailAddress = register.EmailAddress,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Roles = new List<Role> { Role.Student }
                };
                _userRepository.Add(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, register);
                throw ex;
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                var user = _userRepository.GetById(id).Result;
                user.Active = false;
                return _userRepository.Update(user);
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
                entity.UpdationTime = DateTime.UtcNow;
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
