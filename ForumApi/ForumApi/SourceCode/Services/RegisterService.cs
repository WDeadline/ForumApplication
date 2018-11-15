﻿using ForumApi.Helpers;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Repositories;
using ForumApi.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.SourceCode.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ILogger<RegisterService> _logger;
        private readonly IUserRepository _userRepository;
        public RegisterService(ILogger<RegisterService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<bool> IsExistedEmailAddressAsync(string emailAddress)
        {
            var user = await _userRepository.GetUserByEmailAddressAsync(emailAddress);
            return (user != null);
        }

        public async Task<bool> IsExistedUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return (user != null);
        }

        public async Task<bool> RegisterAsync(Register register)
        {
            try
            {
                PasswordManager.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User
                {
                    FirstName = register.FirstName.Trim(),
                    LastName = register.LastName.Trim(),
                    Username = register.Username,
                    EmailAddress = register.EmailAddress,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Roles = new List<string> { "Student" }
                };
                await _userRepository.AddAsync(user);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, register);
                throw e;
            }
        }
    }
}
