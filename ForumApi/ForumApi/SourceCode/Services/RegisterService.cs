using AutoMapper;
using ForumApi.Exeptions;
using ForumApi.Helpers;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public RegisterService(ILogger<RegisterService> logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
           
        }

        public async Task<bool> Register(Register register)
        {
            try
            {
                if (await _userRepository.GetUserByUsernameAsync(register.Username) != null)
                {
                    throw new ConflictException("Sorry, A account with the username {0} already exists.", register.Username);
                }

                if (await _userRepository.GetUserByEmailAddressAsync(register.EmailAddress) != null)
                {
                    throw new ConflictException("Sorry, A account with the email address {0} already exists.", register.EmailAddress);
                }

                byte[] passwordHash, passwordSalt;
                PasswordManager.CreatePasswordHash(register.Password, out passwordHash, out passwordSalt);
                User user = _mapper.Map<User>(register);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Roles = new List<string> { "Student" };
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
