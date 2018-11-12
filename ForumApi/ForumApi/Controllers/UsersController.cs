using ForumApi.Models;
using ForumApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogError("Dit me may");
            return new OkObjectResult(await _userService.Get());
        }
        // GET: api/users/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.Get(id);
            if (user == null)
                return new NotFoundResult();
            return new OkObjectResult(user);
        }
        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            await _userService.Create(user);
            return new OkObjectResult(user);
        }
        // PUT: api/users/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]User user)
        {
            var userFromDb = await _userService.Get(id);
            if (userFromDb == null)
                return new NotFoundResult();
            user.Id = userFromDb.Id;
            await _userService.Update(user);
            return new OkObjectResult(user);
        }
        // DELETE: api/users/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userFromDb = await _userService.Get(id);
            if (userFromDb == null)
                return new NotFoundResult();
            await _userService.Delete(id);
            return new OkResult();
        }
    }
}
