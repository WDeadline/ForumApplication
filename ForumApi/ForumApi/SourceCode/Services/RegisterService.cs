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
            if(register == null)
            {
                throw new BadRequestException("Please enter your Account.");
            }

            if (string.IsNullOrWhiteSpace(register.Password))
            {
                throw new BadRequestException("Please enter your password.");
            }
            
            if (await _userRepository.GetUserByEmailAddress(register.Username) != null)
            {
                throw new ConflictException("Sorry, A account with the username {0} already exists.", register.Username);
            }
            if (await _userRepository.GetUserByEmailAddress(register.EmailAddress) != null)
            {
                throw new ConflictException("Sorry, A account with the email address {0} already exists.", register.EmailAddress);
            }

            byte[] passwordHash, passwordSalt;
            PasswordManager.CreatePasswordHash(register.Password, out passwordHash, out passwordSalt);
            var user = _mapper.Map<User>(register);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Roles = new List<string> { "Student" };
            try
            {
                await _userRepository.Create(user);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
