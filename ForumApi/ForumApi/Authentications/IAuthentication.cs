﻿using ForumApi.Models;
using ForumApi.Payloads;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForumApi.Authentications
{
    public interface IAuthentication
    {
        Task<UserDto> AuthenticateAsync(string usernameOrEmailAddress, string password);
    }
}
