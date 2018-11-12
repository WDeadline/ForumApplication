using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ForumApi.Commons;
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
            string usernameOrEmailAddress = login.UsernameOrEmailAddress.Trim();
            _logger.LogInformation("Login with username or email address is {UsernameOrEmailAddress}", usernameOrEmailAddress);
            bool isEmailAddress = usernameOrEmailAddress.IndexOf('@') > -1;
            if (isEmailAddress)
            {
                Regex regex = new Regex(RegexText.EMAILADDRESSREGEX);
                if (!regex.IsMatch(usernameOrEmailAddress))
                {
                    ModelState.AddModelError("UsernameOrEmailAddress", "Please enter your email address in format: yourname@example.com");
                    _logger.LogError("The Email Address {EmailAddress} is not valid.", usernameOrEmailAddress);
                }
            }
            else
            {
                Regex regex = new Regex(RegexText.USERNAMEREGEX);
                if (!regex.IsMatch(usernameOrEmailAddress))
                {
                    ModelState.AddModelError("UsernameOrEmailAddress", "A username can only contain alphanumeric characters (letters a-Z, numbers 0-9) and cannot be longer than 15 characters.");
                    _logger.LogError("The Username {Username} is not valid.", usernameOrEmailAddress);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _authenticationService.AuthenticateAsync(usernameOrEmailAddress, login.Password);
                if (user == null)
                {
                    if (isEmailAddress)
                    {
                        ModelState.AddModelError("UsernameOrEmailAddress", "Your email address and/or password do not match.");
                        _logger.LogError("EmailAddress and/or password is incorrect");
                    }
                    else
                    {
                        ModelState.AddModelError("UsernameOrEmailAddress", "Your username and/or password do not match.");
                        _logger.LogError("Username and/or password is incorrect");
                    }
                    return BadRequest(ModelState);
                }

                return new ObjectResult(user);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}