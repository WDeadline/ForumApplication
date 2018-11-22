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
    public class SkillsController : ControllerBase
    {
        private readonly ILogger<SkillsController> _logger;
        private readonly IService<Skill> _skillService;
        public SkillsController(ILogger<SkillsController> logger, IService<Skill> skillService)
        {
            _logger = logger;
            _skillService = skillService;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _skillService.GetAll());
        }

        // GET: api/Skills/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var Skill = await _skillService.GetById(id);
            if (Skill == null)
                return new NotFoundResult();
            return new OkObjectResult(Skill);
        }
        // POST: api/Skills
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Skill Skill)
        {
            await _skillService.Add(Skill);
            return new OkObjectResult(Skill);
        }
        // PUT: api/Skills/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Skill Skill)
        {
            var SkillFromDb = await _skillService.GetById(id);
            if (SkillFromDb == null)
                return new NotFoundResult();
            Skill.Id = id;
            await _skillService.Update(Skill);
            return new OkResult();
        }
        // DELETE: api/Skills/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var SkillFromDb = await _skillService.GetById(id);
            if (SkillFromDb == null)
                return new NotFoundResult();
            await _skillService.Delete(id);
            return new OkResult();
        }
    }
}