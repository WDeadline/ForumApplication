using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    }
}
