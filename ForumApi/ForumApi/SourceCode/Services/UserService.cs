using ForumApi.Helpers;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Repositories;
using ForumApi.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.SourceCode.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public Task Create(User obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id) => _userRepository.Delete(new ObjectId(id));

        public Task<IEnumerable<User>> Get() => _userRepository.Get();

        public Task<User> Get(string id) => _userRepository.Get(new ObjectId(id));

        public Task<bool> Register(Register register)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User obj) => _userRepository.Update(obj);
    }
}
