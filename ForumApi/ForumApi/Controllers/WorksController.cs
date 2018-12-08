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
    public class WorksController : ControllerBase
    {
        private readonly ILogger<WorksController> _logger;
        private readonly IRepository<Work> _workRepository;

        public WorksController(ILogger<WorksController> logger, IRepository<Work> workRepository)
        {
            _logger = logger;
            _workRepository = workRepository;
        } 
        // GET: api/Works
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var works = await _workRepository.GetAll();
            return new OkObjectResult(works);
        }

        // GET: api/Works/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var Work = await _workRepository.GetById(id);
            if (Work == null)
                return new NotFoundResult();
            return new OkObjectResult(Work);
        }

        // PUT: api/Works/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Work work)
        {
            var workFromDb = await _workRepository.GetById(id);
            if (workFromDb == null)
                return new NotFoundResult();
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Work work)
        {
            var result = _workRepository.Add(work);
            return new OkObjectResult(result);
        }

        // DELETE: api/Works/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var workFromDb = await _workRepository.GetById(id);
            if (workFromDb == null)
                return new NotFoundResult();
            await _workRepository.Delete(id);
            return new OkResult();
        }
    }
}