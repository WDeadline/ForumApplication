using ForumApi.Interfaces;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        private readonly string PATH_AVATAR;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            PATH_AVATAR = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            foreach(var user in users)
            {
                user.Avatar = !string.IsNullOrEmpty(user.Avatar) ? PATH_AVATAR + user.Avatar : "";
            }
            return new OkObjectResult(users);
        }

        // GET: api/users/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return new NotFoundResult();
            user.Avatar = !string.IsNullOrEmpty(user.Avatar) ? PATH_AVATAR + user.Avatar : "";
            return new OkObjectResult(user);
        }

        // PUT: api/users/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]UpdationUser updationUser)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            _userService.UpdateUser(userFromDb, updationUser);
            return new OkResult();
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreationUser creationUser)
        {
            _logger.LogInformation("create with username \"{0}\" and email address \"{1}\"", creationUser.Username, creationUser.EmailAddress);
            await CheckUniqueEmailAddressAsync(creationUser.EmailAddress);
            await CheckUniqueUsernameAsync(creationUser.Username);
            if (!ModelState.IsValid)
            {
                return Conflict(ModelState);
            }

            var user = _userService.CreateUser(creationUser);
            return new OkObjectResult(user);
        }

        private async Task CheckUniqueEmailAddressAsync(string emailAddress)
        {
            bool isExistedEmailAddress = await _userService.IsExistedEmailAddressAsync(emailAddress);
            if (isExistedEmailAddress)
            {
                ModelState.AddModelError("EmailAddress", "Sorry, A account with the email address \"" + emailAddress + "\" already exists.");
                _logger.LogError("A account with the email address \"" + emailAddress + "\" already exists.");
            }
        }

        private async Task CheckUniqueUsernameAsync(string username)
        {
            bool isExistedUsername = await _userService.IsExistedUsernameAsync(username);
            if (isExistedUsername)
            {
                ModelState.AddModelError("Username", "Sorry, A account with the username \"" + username + "\" already exists.");
                _logger.LogError("A account with the username \"" + username + "\" already exists.");
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            await _userService.Delete(id);
            return new OkResult();
        }
    }
}
