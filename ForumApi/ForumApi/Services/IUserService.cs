using ForumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public interface IUserService : IService<User>
    {
        Task<User> AuthenticateAsync(string usernameOrEmailAddress, string password);
    }
}
