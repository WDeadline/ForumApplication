using ForumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAddressAsync(string emailAddress);
        Task<User> GetUserByUsernameOrEmailAddressAsync(string usernameOrEmailAddress);
    }
}
