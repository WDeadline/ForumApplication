using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly ILogger<WorksController> _logger;
        private readonly IRepository<Work> _workRepository;
        private readonly IUserService _userService;

        public WorksController(ILogger<WorksController> logger, IRepository<Work> workRepository, IUserService userService)
        {
            _logger = logger;
            _workRepository = workRepository;
            _userService = userService;
        } 
        // GET: api/Works
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var works = await _workRepository.GetAll();
            return new OkObjectResult(GetWorksView(works).Result);
        }

        // GET: api/works/companies/companyId
        [HttpGet("companies/{companyId:length(24)}")]
        public async Task<IActionResult> GetWorksByCompanyId(string companyId)
        {
            var works = await _workRepository.GetMany(w => w.CompanyId == companyId);
            return new OkObjectResult(GetWorksView(works).Result);
        }


        // GET: api/Works/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var work = await _workRepository.GetById(id);
            if (work == null)
                return new NotFoundResult();
            return new OkObjectResult(GetWorkView(work).Result);
        }

        public async Task<List<object>> GetWorksView(IEnumerable<Work> works)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            List<object> worksView = new List<object>();

            foreach (var work in works)
            {
                worksView.Add(GetWorkView(work).Result);
            }
            return worksView;
        }

        public async Task<object> GetWorkView(Work work)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            var userFromDb = await _userService.GetById(work.CompanyId);
            object workView = new
            {
                id = work.Id,
                companyId = work.CompanyId,
                userView = new
                {
                    id = userFromDb.Id,
                    name = userFromDb.Name,
                    avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                },
                title = work.Title,
                position = work.Position,
                description = work.Description,
                address = work.Address,
                salary = work.Salary,
                tags = work.Tags,
                updationTime = work.UpdationTime
            };
            return workView;
        }

        // PUT: api/Works/id
        [Authorize(Roles = "Company, Administrator")]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Work work)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var workFromDb = await _workRepository.GetById(id);
            if (workFromDb == null)
                return new NotFoundResult();
            if (!(workFromDb.CompanyId.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            workFromDb.Position = work.Position;
            workFromDb.Salary = work.Salary;
            workFromDb.Title = work.Title;
            workFromDb.Tags = work.Tags;
            workFromDb.Address = work.Address;
            workFromDb.Description = work.Description;
            workFromDb.UpdationTime = DateTime.UtcNow;
            _workRepository.Update(workFromDb);
            return new OkResult();
        }

        // POST: api/Works
        [Authorize(Roles = "Company, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Work work)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            work.CompanyId = userId;
            _workRepository.Add(work);
            return new OkObjectResult(GetWorkView(work).Result);
        }

        [Authorize(Roles = "Company, Administrator")]
        // DELETE: api/Works/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var workFromDb = await _workRepository.GetById(id);
            if (workFromDb == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!(workFromDb.CompanyId.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            await _workRepository.Delete(id);
            return new OkResult();
        }
    }
}