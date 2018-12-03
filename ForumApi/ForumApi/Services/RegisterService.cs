using ForumApi.Helpers;
using ForumApi.Interfaces;
using ForumApi.Interfaces.Services;
using ForumApi.Models;
using ForumApi.Payloads;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ILogger<RegisterService> _logger;
        private readonly IRepository<User> _userRepository;

        public RegisterService(ILogger<RegisterService> logger, IRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
    }
}
