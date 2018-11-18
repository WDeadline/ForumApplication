using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        // GET api/tests/student
        [Authorize(Roles = "Student, Teacher, Admin")]
        [HttpGet, Route("student")]
        public IActionResult Student()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh", Discription = ""}, new { Name = "Student"} });
        }

        // GET api/tests/teacher
        [Authorize(Roles = "Teacher, Admin")]
        [HttpGet, Route("teacher")]
        public IActionResult Teacher()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Cong" } });
        }

        // GET api/tests/admin
        [Authorize(Roles = "Admin")]
        [HttpGet, Route("admin")]
        public IActionResult Admin()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Admin" } });
        }

        // GET api/tests/anonymous
        [Authorize]
        [HttpGet, Route("authorize")]
        public IActionResult Authorizes()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Authorize" } });
        }


        // GET api/tests/anonymous
        [AllowAnonymous]
        [HttpGet, Route("anonymous")]
        public IActionResult Anonymous()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Anonymous" } });
        }

        [AllowAnonymous]
        [HttpGet, Route("questions")]
        public IActionResult GetQuestions()
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Anonymous" } });
        }


        [AllowAnonymous]
        [HttpGet, Route("upload")]
        public IActionResult UploadImage(IFormFile file)
        {
            return new OkObjectResult(new List<object> { new { Name = "Thanh" }, new { Name = "Anonymous" } });
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                                                                  //for the file due to security reasons.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }
    }
}
