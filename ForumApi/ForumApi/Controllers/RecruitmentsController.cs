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
    public class RecruitmentsController : ControllerBase
    {
        private readonly ILogger<RecruitmentsController> _logger;
        private readonly IRepository<Recruitment> _recruitmentRepository;

        public RecruitmentsController(ILogger<RecruitmentsController> logger, IRepository<Recruitment> recruitmentRepository)
        {
            _logger = logger;
            _recruitmentRepository = recruitmentRepository;
        }
        // GET: api/recruitments
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recruitments = await _recruitmentRepository.GetAll();
            return new OkObjectResult(recruitments);
        }

        // GET: api/recruitments/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var recruitment = await _recruitmentRepository.GetById(id);
            if (recruitment == null)
                return new NotFoundResult();
            return new OkObjectResult(recruitment);
        }

        // PUT: api/recruitments/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Recruitment recruitment)
        {
            var recruitmentFromDb = await _recruitmentRepository.GetById(id);
            if (recruitmentFromDb == null)
                return new NotFoundResult();
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Recruitment recruitment)
        {
            var result = _recruitmentRepository.Add(recruitment);
            return new OkObjectResult(result);
        }

        // DELETE: api/recruitments/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var recruitmentFromDb = await _recruitmentRepository.GetById(id);
            if (recruitmentFromDb == null)
                return new NotFoundResult();
            await _recruitmentRepository.Delete(id);
            return new OkResult();
        }
    }
}