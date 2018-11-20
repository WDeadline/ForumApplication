using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IImageHandler _imageHandler;
        private readonly IHostingEnvironment _env;

        public ProfilesController(IImageHandler imageHandler, IHostingEnvironment env)
        {
            _imageHandler = imageHandler;
            _env = env;
        }


        // GET api/profiles
        [HttpGet, Route("teacher")]
        public IActionResult Get()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Cong" } });
        }

        [HttpPost, Route("avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            string userId = User?.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            string e = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/");
            string g = HttpContext.Request.ToString();
            var img = await _imageHandler.UploadAvatar(userId, file);
            img = img.Replace("\\", "/");
            string path = e + img;

            return new OkObjectResult(path);
        }

    }
}