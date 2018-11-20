using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Interfaces.Services;
using ForumApi.Payloads;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                _logger.LogInformation("Register with username \"{0}\" and email address \"{1}\"", register.Username, register.EmailAddress);
                await CheckUniqueEmailAddressAsync(register.EmailAddress);
                await CheckUniqueUsernameAsync(register.Username);
                if (!ModelState.IsValid)
                {
                    return Conflict(ModelState);
                }
                _registerService.RegisterAsync(register);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, register);
                throw e;
            }
        }

        private async Task CheckUniqueEmailAddressAsync(string emailAddress)
        {
            bool isExistedEmailAddress = await _registerService.IsExistedEmailAddressAsync(emailAddress);
            if (isExistedEmailAddress)
            {
                ModelState.AddModelError("EmailAddress", "Sorry, A account with the email address \"" + emailAddress + "\" already exists.");
                _logger.LogError("A account with the email address \"" + emailAddress + "\" already exists.");
            }
        }

        private async Task CheckUniqueUsernameAsync(string username)
        {
            bool isExistedUsername = await _registerService.IsExistedUsernameAsync(username);
            if (isExistedUsername)
            {
                ModelState.AddModelError("Username", "Sorry, A account with the username \"" + username + "\" already exists.");
                _logger.LogError("A account with the username \"" + username + "\" already exists.");
            }
        }
    }
}