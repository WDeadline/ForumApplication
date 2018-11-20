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
        Task Add(User user);
        Task<bool> Delete(string id);
        Task<bool> Update(User user);
        Task<User> GetById(string id);
        Task<IEnumerable<User>> GetAll();
        Task<UserDto> Login(string usernameOrEmailAddress, string password);
        Task<bool> Register(Register register);
        Task<bool> IsExistedUsername(string username);
        Task<bool> IsExistedEmailAddress(string emailAddress);
    }
}
