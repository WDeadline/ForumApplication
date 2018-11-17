﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Interfaces.Services;
using ForumApi.Payloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ForumApi.Authencations.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(ILogger<LoginController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        // POST: api/login
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Login login)
        {
            try
            {
                string usernameOrEmailAddress = login.UsernameOrEmailAddress.Trim();
                _logger.LogInformation("Login with Username or EmailAddress is \"{0}\"", usernameOrEmailAddress);
                var user = await _authenticationService.AuthenticateAsync(usernameOrEmailAddress, login.Password);
                if (user == null)
                {
                    AddLogging(usernameOrEmailAddress);
                    return Unauthorized();
                }
                return new OkObjectResult(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, login);
                throw e;
            }
        }

        private void AddLogging(string usernameOrEmailAddress)
        {
            bool isEmailAddress = usernameOrEmailAddress.IndexOf('@') > -1;
            if (isEmailAddress)
            {
                _logger.LogError("EmailAddress and/or password is incorrect");
            }
            else
            {
                _logger.LogError("Username and/or password is incorrect");
            }
        }
    }
}