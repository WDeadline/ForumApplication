﻿using ForumApi.Helpers;
using ForumApi.Interfaces.Repositories;
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
        private readonly IUserRepository _userRepository;

        private const string DefaultRole = "Student";
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
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Username = register.Username,
                    EmailAddress = register.EmailAddress,
                    Passwords = new List<Password>() {
                        new Password
                        {
                            PasswordHash = passwordHash,
                            PasswordSalt = passwordSalt
                        }
                    },
                    Roles = new List<string> { DefaultRole }
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
