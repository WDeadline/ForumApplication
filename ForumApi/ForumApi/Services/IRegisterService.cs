using ForumApi.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public interface IRegisterService
    {
        Task<bool> Register(Register register);
    }
}
