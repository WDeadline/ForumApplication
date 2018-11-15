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
            try
            {
                string usernameOrEmailAddress = login.UsernameOrEmailAddress.Trim();
                _logger.LogInformation("Login with Username or EmailAddress is \"{0}\"", usernameOrEmailAddress);

                ValidateUsernameOrEmaillAddress(usernameOrEmailAddress);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _authenticationService.AuthenticateAsync(usernameOrEmailAddress, login.Password);
                if (user == null)
                {
                    AddModelErrorUsernameOrEmaillAddress(usernameOrEmailAddress);
                    return BadRequest(ModelState);
                }

                return new OkObjectResult(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, login);
                throw e;
            }
        }

        private void ValidateUsernameOrEmaillAddress(string usernameOrEmailAddress)
        {
            bool isEmailAddress = usernameOrEmailAddress.IndexOf('@') > -1;
            if (isEmailAddress)
            {
                ValidateEmailAddress(usernameOrEmailAddress);
            }
            else
            {
                ValidateUsername(usernameOrEmailAddress);
            }
        }

        private void ValidateEmailAddress(string emailAddress)
        {
            Regex regex = new Regex(RegexText.EMAILADDRESSREGEX);
            if (!regex.IsMatch(emailAddress))
            {
                ModelState.AddModelError("UsernameOrEmailAddress", "Please enter your email address in format: yourname@example.com");
                _logger.LogError("The Email Address {EmailAddress} is not valid.", emailAddress);
            }
        }

        private void ValidateUsername(string username)
        {
            Regex regex = new Regex(RegexText.USERNAMEREGEX);
            if (!regex.IsMatch(username))
            {
                ModelState.AddModelError("UsernameOrEmailAddress", "A username can only contain alphanumeric characters (letters a-Z, numbers 0-9) and cannot be longer than 20 characters.");
                _logger.LogError("The Username {Username} is not valid.", username);
            }
        }

        private void AddModelErrorUsernameOrEmaillAddress(string usernameOrEmailAddress)
        {
            bool isEmailAddress = usernameOrEmailAddress.IndexOf('@') > -1;
            if (isEmailAddress)
            {
                AddModelErrorEmaillAddress(usernameOrEmailAddress);
            }
            else
            {
                AddModelErrorUsername(usernameOrEmailAddress);
            }
        }

        private void AddModelErrorEmaillAddress(string emailAddress)
        {
            ModelState.AddModelError("UsernameOrEmailAddress", "Your email address and/or password do not match.");
            _logger.LogError("EmailAddress and/or password is incorrect");
        }

        private void AddModelErrorUsername(string username)
        {
            ModelState.AddModelError("UsernameOrEmailAddress", "Your username and/or password do not match.");
            _logger.LogError("Username and/or password is incorrect");
        }
    }
}