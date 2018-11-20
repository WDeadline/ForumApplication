using ForumApi.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<CurrentUser> AuthenticateAsync(string usernameOrEmailAddress, string password);
    }
}
