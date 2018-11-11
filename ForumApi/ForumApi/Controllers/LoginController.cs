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
                    ModelState.AddModelError("UsernameOrEmailAddress", "The Email Address field is not valid.");
                    _logger.LogError("The Email Address {EmailAddress} is not valid.", usernameOrEmailAddress);
                }
            }
            else
            {
                Regex regex = new Regex(RegexText.USERNAMEREGEX);
                if (!regex.IsMatch(usernameOrEmailAddress))
                {
                    ModelState.AddModelError("UsernameOrEmailAddress", "The Username field is not valid.");
                    _logger.LogError("The Username {Username} is not valid.", usernameOrEmailAddress);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authenticationService.AuthenticateAsync(usernameOrEmailAddress, login.Password);
            if (user == null)
            {
                if (isEmailAddress)
                {
                    ModelState.AddModelError("UsernameOrEmailAddress", "Email address or password is incorrect");
                    _logger.LogError("EmailAddress or password is incorrect");
                }
                else
                {
                    ModelState.AddModelError("UsernameOrEmailAddress", "Username or password is incorrect");
                    _logger.LogError("Username or password is incorrect");
                }
                return BadRequest(ModelState);
            }

            return new ObjectResult(user);
        }
    }
}