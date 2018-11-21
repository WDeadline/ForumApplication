using ForumApi.Interfaces;
using ForumApi.Models;
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
        private readonly IService<Information> _informationService;
        private readonly IService<Education> _educationService;
        private readonly IService<Experience> _experienceService;
        private readonly IService<Objective> _objectiveService;
        private readonly IService<Question> _questionService;
        public UsersController(ILogger<UsersController> logger, IUserService userService,
            IService<Education> educationService, IService<Information> informationService,
            IService<Objective> objectiveService, IService<Question> questionService,
            IService<Experience> experienceService)
        {
            _logger = logger;
            _userService = userService;
            _educationService = educationService;
            _informationService = informationService;
            _objectiveService = objectiveService;
            _questionService = questionService;
            _experienceService = experienceService;

        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _userService.GetAll());
        }

        // GET: api/users/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return new NotFoundResult();
            return new OkObjectResult(user);
        }

        // GET: api/users/id/educations
        [HttpGet("{id:length(24)}/educations")]
        public async Task<IActionResult> GetEducations(string id)
        {
            var educations = await ((EducationService)_educationService).GetByUserId(id);
            return new OkObjectResult(educations);
        }

        // GET: api/users/id/experiences
        [HttpGet("{id:length(24)}/experiences")]
        public async Task<IActionResult> GetExperiences(string id)
        {
            var experiences = await ((ExperienceService)_experienceService).GetByUserId(id);
            return new OkObjectResult(experiences);
        }

        // GET: api/users/id/information
        [HttpGet("{id:length(24)}/information")]
        public async Task<IActionResult> GetInformation(string id)
        {
            var information = await ((InformationService)_informationService).GetByUserId(id);
            return new OkObjectResult(information);
        }

        // GET: api/users/id/objectives
        [HttpGet("{id:length(24)}/objectives")]
        public async Task<IActionResult> GetObjectives(string id)
        {
            var objectives = await ((ObjectiveService)_objectiveService).GetByUserId(id);
            return new OkObjectResult(objectives);
        }

        // GET: api/users/id/questions
        [HttpGet("{id:length(24)}/questions")]
        public async Task<IActionResult> GetQuestions(string id)
        {
            var questions = await ((QuestionService)_questionService).GetByUserId(id);
            return new OkObjectResult(questions);
        }


        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            await _userService.Add(user);
            return new OkObjectResult(user);
        }
        // PUT: api/users/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]User user)
        {
            /*var userFromDb = await _userService.GetByIdAsync(id);
            if (userFromDb == null)
                return new NotFoundResult();
            user.Id = userFromDb.Id;
            Password password = new Password
            {
                PasswordHash = userFromDb.PasswordHash,
                PasswordSalt = userFromDb.PasswordSalt
            }
            user.PasswordHash = userFromDb.PasswordHash;
            user.PasswordSalt = userFromDb.PasswordSalt;
            user.UpdationTime = DateTime.UtcNow;
            await _userService.UpdateAsync(user);*/
            return new OkResult();
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
