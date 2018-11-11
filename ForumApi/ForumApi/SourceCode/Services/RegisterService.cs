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
        public Task<bool> Register(Register register)
        {
            throw new NotImplementedException();
        }
    }
}
