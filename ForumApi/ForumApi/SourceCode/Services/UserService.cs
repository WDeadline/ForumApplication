using ForumApi.Helpers;
using ForumApi.Models;
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

        public Task Create(User obj) => _userRepository.Create(obj);

        public Task<bool> Delete(string id) => _userRepository.Delete(new ObjectId(id));

        public Task<IEnumerable<User>> Get() => _userRepository.Get();

        public Task<User> Get(string id) => _userRepository.Get(new ObjectId(id));

        public async Task<User> AuthenticateAsync(string usernameOrEmailAddress, string password)
        {
            if (string.IsNullOrEmpty(usernameOrEmailAddress) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userRepository.GetUserByUsernameOrEmailAddress(usernameOrEmailAddress);

            if (user == null)
                return null;

            if (!PasswordManager.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public Task<bool> Update(User obj) => _userRepository.Update(obj);
    }
}
