using ForumApi.Interfaces;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        #region user
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            foreach (var user in users)
            {
                user.Avatar = !string.IsNullOrEmpty(user.Avatar) ? path + user.Avatar : "";
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
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            user.Avatar = !string.IsNullOrEmpty(user.Avatar) ? path + user.Avatar : "";
            return new OkObjectResult(user);
        }

        // PUT: api/users/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]UpdationUser updationUser)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            userFromDb.Name = updationUser.Name;
            userFromDb.Birthday = updationUser.Birthday;
            userFromDb.PhoneNumber = updationUser.PhoneNumber;
            userFromDb.Address = updationUser.Address;
            userFromDb.Position = updationUser.Position;
            userFromDb.UpdationTime = DateTime.UtcNow;
            _userService.Update(userFromDb);
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
            return new OkObjectResult(user.Result);
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
        #endregion

        #region skills
        // POST: api/users/userId/skills
        [HttpGet("{id:length(24)}/skills")]
        public async Task<IActionResult> GetSkills(string id)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            return new OkObjectResult(userFromDb.Skills);
        }

        // POST: api/users/userId/skills
        [HttpPost("{id:length(24)}/skills")]
        public async Task<IActionResult> PostSkill(string id, [FromBody]Skill skill)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            if(userFromDb.Skills.Any(s => s.Name == skill.Name))
            {
                ModelState.AddModelError("Name", "Sorry, A skill with the name \"" + skill.Name + "\" already exists.");
                return Conflict(ModelState);
            }
            skill.Id = ObjectId.GenerateNewId().ToString();
            userFromDb.Skills.Add(skill);
            _userService.Update(userFromDb);
            return new OkObjectResult(skill);
        }

        // PUT: api/users/userId/skills/skillId 
        [HttpPut("{id:length(24)}/skills/{skillId:length(24)}")]
        public async Task<IActionResult> PutSkill(string id, string skillId, [FromBody]Skill skill)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Skill userSkill = userFromDb.Skills.FirstOrDefault(s => s.Id == skillId);
            if (userSkill == null)
                return new NotFoundResult();
            if(!userSkill.Name.Equals(skill.Name))
            {
                if (userFromDb.Skills.Any(s => s.Name == skill.Name))
                {
                    ModelState.AddModelError("Name", "Sorry, A skill with the name \"" + skill.Name + "\" already exists.");
                    return Conflict(ModelState);
                }
            }
            userSkill.Name = skill.Name;
            userSkill.Level = skill.Level;
            userSkill.IsTechnical = skill.IsTechnical;
            userSkill.UpdationTime = DateTime.UtcNow;
            _userService.Update(userFromDb);
            return new OkObjectResult(userSkill);
        }

        // PUT: api/users/userId/skills/skillId 
        [HttpDelete("{id:length(24)}/skills/{skillId:length(24)}")]
        public async Task<IActionResult> DeleteSkill(string id, string skillId)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Skill userSkill = userFromDb.Skills.FirstOrDefault(s => s.Id == skillId);
            if (userSkill == null)
                return new NotFoundResult();
            userFromDb.Skills.Remove(userSkill);
            _userService.Update(userFromDb);
            return new OkResult();
        }
        #endregion

        #region objectives
        // POST: api/users/userId/objectives
        [HttpGet("{id:length(24)}/objectives")]
        public async Task<IActionResult> GetObjectives(string id)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            return new OkObjectResult(userFromDb.Objectives);
        }

        // POST: api/users/userId/objectives
        [HttpPost("{id:length(24)}/objectives")]
        public async Task<IActionResult> PostObjective(string id, [FromBody]Objective objective)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            objective.Id = ObjectId.GenerateNewId().ToString();
            userFromDb.Objectives.Add(objective);
            _userService.Update(userFromDb);
            return new OkObjectResult(objective);
        }

        // PUT: api/users/userId/objectives/objectiveId 
        [HttpPut("{id:length(24)}/objectives/{objectiveId:length(24)}")]
        public async Task<IActionResult> PutObjective(string id, string objectiveId, [FromBody]Objective objective)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Objective userObjective = userFromDb.Objectives.FirstOrDefault(s => s.Id == objectiveId);
            if(userObjective == null)
                return new NotFoundResult();
            userObjective.Description = objective.Description;
            userObjective.UpdationTime = DateTime.UtcNow;
            _userService.Update(userFromDb);
            return new OkObjectResult(userObjective);
        }

        // PUT: api/users/userId/objectives/objectiveId 
        [HttpDelete("{id:length(24)}/objectives/{objectiveId:length(24)}")]
        public async Task<IActionResult> DeleteObjective(string id, string objectiveId)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Objective userObjective = userFromDb.Objectives.FirstOrDefault(s => s.Id == objectiveId);
            if (userObjective == null)
                return new NotFoundResult();
            userFromDb.Objectives.Remove(userObjective);
            _userService.Update(userFromDb);
            return new OkResult();
        }

        #endregion

        #region education
        // POST: api/users/userId/educations
        [HttpGet("{id:length(24)}/educations")]
        public async Task<IActionResult> GetEducations(string id)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            return new OkObjectResult(userFromDb.Educations);
        }

        // POST: api/users/userId/educations
        [HttpPost("{id:length(24)}/educations")]
        public async Task<IActionResult> PostEducation(string id, [FromBody]Education education)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            education.Id = ObjectId.GenerateNewId().ToString();
            userFromDb.Educations.Add(education);
            _userService.Update(userFromDb);
            return new OkObjectResult(education);
        }

        // PUT: api/users/userId/educations/educationId 
        [HttpPut("{id:length(24)}/educations/{educationId:length(24)}")]
        public async Task<IActionResult> PutEducation(string id, string educationId, [FromBody]Education education)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Education userEducation = userFromDb.Educations.FirstOrDefault(edu => edu.Id == educationId);
            if(userEducation == null)
                return new NotFoundResult();
            userEducation.Major = education.Major;
            userEducation.University = education.University;
            userEducation.StartTime = education.StartTime;
            userEducation.EndTime = education.EndTime;
            userEducation.GPA = education.GPA;
            userEducation.UpdationTime = DateTime.UtcNow;
            _userService.Update(userFromDb);
            return new OkObjectResult(userEducation);
        }

        // PUT: api/users/userId/educations/educationId 
        [HttpDelete("{id:length(24)}/educations/{educationId:length(24)}")]
        public async Task<IActionResult> DeleteEducation(string id, string educationId)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Education userEducation = userFromDb.Educations.FirstOrDefault(edu => edu.Id == educationId);
            if (userEducation == null)
                return new NotFoundResult();
            userFromDb.Educations.Remove(userEducation);
            _userService.Update(userFromDb);
            return new OkResult();
        }
        #endregion

        #region experience
        // POST: api/users/userId/experiences
        [HttpGet("{id:length(24)}/experiences")]
        public async Task<IActionResult> GetExperiences(string id)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            return new OkObjectResult(userFromDb.Experiences);
        }

        // POST: api/users/userId/experiences
        [HttpPost("{id:length(24)}/experiences")]
        public async Task<IActionResult> PostExperience(string id, [FromBody]Experience experience)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            experience.Id = ObjectId.GenerateNewId().ToString();
            userFromDb.Experiences.Add(experience);
            _userService.Update(userFromDb);
            return new OkObjectResult(experience);
        }

        // PUT: api/users/userId/experiences/experienceId 
        [HttpPut("{id:length(24)}/experiences/{experienceId:length(24)}")]
        public async Task<IActionResult> PutExperience(string id, string experienceId, [FromBody]Experience experience)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Experience userExperience = userFromDb.Experiences.FirstOrDefault(s => s.Id == experienceId);
            if (userExperience == null)
                return new NotFoundResult();
            userExperience.Workplace = experience.Workplace;
            userExperience.Position = experience.Position;
            userExperience.StartTime = experience.StartTime;
            userExperience.EndTime = experience.EndTime;
            userExperience.Description = experience.Description;
            userExperience.UpdationTime = DateTime.UtcNow;
            _userService.Update(userFromDb);
            return new OkObjectResult(userExperience);
        }

        // PUT: api/users/userId/experiences/experienceId 
        [HttpDelete("{id:length(24)}/experiences/{experienceId:length(24)}")]
        public async Task<IActionResult> DeleteExperience(string id, string experienceId)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Experience userExperience = userFromDb.Experiences.FirstOrDefault(s => s.Id == experienceId);
            if (userExperience == null)
                return new NotFoundResult();
            userFromDb.Experiences.Remove(userExperience);
            _userService.Update(userFromDb);
            return new OkResult();
        }
        #endregion

        #region activity
        // POST: api/users/userId/activities
        [HttpGet("{id:length(24)}/activities")]
        public async Task<IActionResult> GetActivities(string id)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            return new OkObjectResult(userFromDb.Activities);
        }

        // POST: api/users/userId/activities
        [HttpPost("{id:length(24)}/activities")]
        public async Task<IActionResult> PostActivity(string id, [FromBody]Activity activity)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            activity.Id = ObjectId.GenerateNewId().ToString();
            userFromDb.Activities.Add(activity);
            _userService.Update(userFromDb);
            return new OkObjectResult(activity);
        }

        // PUT: api/users/userId/activities/activityId 
        [HttpPut("{id:length(24)}/activities/{activityId:length(24)}")]
        public async Task<IActionResult> PutActivity(string id, string activityId, [FromBody]Activity activity)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Activity userActivity = userFromDb.Activities.FirstOrDefault(a => a.Id == activityId);
            if (userActivity == null)
                return new NotFoundResult();
            userActivity.Name = activity.Name;
            userActivity.JoinDate = activity.JoinDate;
            _userService.Update(userFromDb);
            return new OkObjectResult(userActivity);
        }

        // PUT: api/users/userId/activities/activityId 
        [HttpDelete("{id:length(24)}/activities/{activityId:length(24)}")]
        public async Task<IActionResult> DeleteActivity(string id, string activityId)
        {
            var userFromDb = await _userService.GetById(id);
            if (userFromDb == null)
                return new NotFoundResult();
            Activity userActivity = userFromDb.Activities.FirstOrDefault(a => a.Id == activityId);
            if (userActivity == null)
                return new NotFoundResult();
            userFromDb.Activities.Remove(userActivity);
            _userService.Update(userFromDb);
            return new OkResult();
        }

        #endregion
    }
}
