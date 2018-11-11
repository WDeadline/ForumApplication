using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Payloads;
using ForumApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ForumApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IRegisterService _registerService;

        public RegisterController(ILogger<RegisterController> logger, IRegisterService registerService)
        {
            _logger = logger;
            _registerService = registerService;
        }
        // POST: api/register
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Register register)
        {
            await _registerService.Register(register);
            return null;
        }
    }
}