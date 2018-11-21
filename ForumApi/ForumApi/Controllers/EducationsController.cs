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
    public class EducationsController : ControllerBase
    {
        private readonly ILogger<EducationsController> _logger;
        private readonly IService<Education> _educationService;
        public EducationsController(ILogger<EducationsController> logger, IService<Education> educationService)
        {
            _logger = logger;
            _educationService = educationService;
        }

        // GET: api/educations
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _educationService.GetAll());
        }

        // GET: api/educations/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var Education = await _educationService.GetById(id);
            if (Education == null)
                return new NotFoundResult();
            return new OkObjectResult(Education);
        }
        // POST: api/educations
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Education education)
        {
            await _educationService.Add(education);
            return new OkObjectResult(education);
        }
        // PUT: api/educations/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Education education)
        {
            var educationFromDb = await _educationService.GetById(id);
            if (educationFromDb == null)
                return new NotFoundResult();
            education.Id = id;
            await _educationService.Update(education);
            return new OkResult();
        }
        // DELETE: api/educations/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var educationFromDb = await _educationService.GetById(id);
            if (educationFromDb == null)
                return new NotFoundResult();
            await _educationService.Delete(id);
            return new OkResult();
        }
    }
}