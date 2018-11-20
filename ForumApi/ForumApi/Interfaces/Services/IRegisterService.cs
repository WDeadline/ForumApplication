using ForumApi.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces.Services
{
    public interface IRegisterService
    {
        bool RegisterAsync(Register register);
        Task<bool> IsExistedUsernameAsync(string username);
        Task<bool> IsExistedEmailAddressAsync(string emailAddress);
    }
}
