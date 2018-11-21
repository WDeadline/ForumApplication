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
    public class ObjectivesController : ControllerBase
    {
        private readonly ILogger<ObjectivesController> _logger;
        private readonly IService<Objective> _objectiveService;
        public ObjectivesController(ILogger<ObjectivesController> logger, IService<Objective> objectiveService)
        {
            _logger = logger;
            _objectiveService = objectiveService;
        }

        // GET: api/Objectives
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _objectiveService.GetAll());
        }

        // GET: api/Objectives/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var Objective = await _objectiveService.GetById(id);
            if (Objective == null)
                return new NotFoundResult();
            return new OkObjectResult(Objective);
        }
        // POST: api/Objectives
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Objective Objective)
        {
            await _objectiveService.Add(Objective);
            return new OkObjectResult(Objective);
        }
        // PUT: api/Objectives/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Objective objective)
        {
            var objectiveFromDb = await _objectiveService.GetById(id);
            if (objectiveFromDb == null)
                return new NotFoundResult();
            objective.Id = id;
            await _objectiveService.Update(objective);
            return new OkResult();
        }
        // DELETE: api/Objectives/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var objectiveFromDb = await _objectiveService.GetById(id);
            if (objectiveFromDb == null)
                return new NotFoundResult();
            await _objectiveService.Delete(id);
            return new OkResult();
        }
    }
}