using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly ILogger<ExperiencesController> _logger;
        private readonly IService<Experience> _experienceService;
        public ExperiencesController(ILogger<ExperiencesController> logger, IService<Experience> experienceService)
        {
            _logger = logger;
            _experienceService = experienceService;
        }

        // GET: api/Experiences
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _experienceService.GetAll());
        }

        // GET: api/Experiences/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var experience = await _experienceService.GetById(id);
            if (experience == null)
                return new NotFoundResult();
            return new OkObjectResult(experience);
        }
        // POST: api/Experiences
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Experience experience)
        {
            await _experienceService.Add(experience);
            return new OkObjectResult(experience);
        }
        // PUT: api/Experiences/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Experience experience)
        {
            var experienceFromDb = await _experienceService.GetById(id);
            if (experienceFromDb == null)
                return new NotFoundResult();
            experience.Id = id;
            await _experienceService.Update(experience);
            return new OkResult();
        }
        // DELETE: api/Experiences/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ExperienceFromDb = await _experienceService.GetById(id);
            if (ExperienceFromDb == null)
                return new NotFoundResult();
            await _experienceService.Delete(id);
            return new OkResult();
        }
    }
}