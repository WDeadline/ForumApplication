﻿using ForumApi.Models;
using ForumApi.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(string id);
        Task<IEnumerable<User>> GetAll();
        Task<bool> Update(User user);
        Task<User> CreateUser(CreationUser creationUser);
        bool RegisterAsync(Register register);
        Task<bool> IsExistedUsernameAsync(string username);
        Task<bool> IsExistedEmailAddressAsync(string emailAddress);
        Task<bool> Delete(string id);
    }
}
