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
    public class RecruitmentsController : ControllerBase
    {
        private readonly ILogger<RecruitmentsController> _logger;
        private readonly IRepository<Recruitment> _recruitmentRepository;
        private readonly IUserService _userService;

        public RecruitmentsController(ILogger<RecruitmentsController> logger, IRepository<Recruitment> recruitmentRepository, IUserService userService)
        {
            _logger = logger;
            _recruitmentRepository = recruitmentRepository;
            _userService = userService;
        }
        // GET: api/recruitments
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recruitments = await _recruitmentRepository.GetAll();
            return new OkObjectResult(GetRecruitmentsView(recruitments).Result);
        }

        // GET: api/recruitments/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var recruitment = await _recruitmentRepository.GetById(id);
            if (recruitment == null)
                return new NotFoundResult();
            return new OkObjectResult(GetRecruitmentView(recruitment).Result);
        }

        public async Task<List<object>> GetRecruitmentsView(IEnumerable<Recruitment> recruitments)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            List<object> worksView = new List<object>();

            foreach (var recruitment in recruitments)
            {
                worksView.Add(GetRecruitmentView(recruitment).Result);
            }
            return worksView;
        }

        public async Task<object> GetRecruitmentView(Recruitment recruitment)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            var userFromDb = await _userService.GetById(recruitment.CompanyId);
            object workView = new
            {
                id = recruitment.Id,
                userView = new
                {
                    id = userFromDb.Id,
                    name = userFromDb.Name,
                    avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                },
                title = recruitment.Title,
                place = recruitment.Place,
                description = recruitment.Description,
                startTime = recruitment.StartTime,
                endTime = recruitment.EndTime,
                tags = recruitment.Tags,
                updationTime = recruitment.UpdationTime
            };
            return workView;
        }

        // PUT: api/recruitments/id
        [Authorize(Roles = "Company, Administrator")]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Recruitment recruitment)
        {
            var recruitmentFromDb = await _recruitmentRepository.GetById(id);
            if (recruitmentFromDb == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!(recruitmentFromDb.CompanyId.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            recruitmentFromDb.Place = recruitment.Place;
            recruitmentFromDb.EndTime = recruitment.EndTime;
            recruitmentFromDb.Title = recruitment.Title;
            recruitmentFromDb.Tags = recruitment.Tags;
            recruitmentFromDb.StartTime = recruitment.StartTime;
            recruitmentFromDb.Description = recruitment.Description;
            recruitmentFromDb.UpdationTime = DateTime.UtcNow;
            _recruitmentRepository.Update(recruitmentFromDb);
            return new OkResult();
        }

        // POST: api/recruitments
        [Authorize(Roles = "Company, Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Recruitment recruitment)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            recruitment.CompanyId = userId;
            var result = _recruitmentRepository.Add(recruitment);
            return new OkObjectResult(GetRecruitmentView(recruitment).Result);
        }

        // DELETE: api/recruitments/5
        [Authorize(Roles = "Company, Administrator")]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var recruitmentFromDb = await _recruitmentRepository.GetById(id);
            if (recruitmentFromDb == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!(recruitmentFromDb.CompanyId.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            await _recruitmentRepository.Delete(id);
            return new OkResult();
        }
    }
}