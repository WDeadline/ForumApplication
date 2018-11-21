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
    public class InformationsController : ControllerBase
    {
        private readonly ILogger<InformationsController> _logger;
        private readonly IService<Information> _informationService;
        public InformationsController(ILogger<InformationsController> logger, IService<Information> informationService)
        {
            _logger = logger;
            _informationService = informationService;
        }

        // GET: api/Informations
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _informationService.GetAll());
        }

        // GET: api/Informations/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var Information = await _informationService.GetById(id);
            if (Information == null)
                return new NotFoundResult();
            return new OkObjectResult(Information);
        }
        // POST: api/Informations
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Information information)
        {
            await _informationService.Add(information);
            return new OkObjectResult(information);
        }
        // PUT: api/Informations/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Information information)
        {
            var informationFromDb = await _informationService.GetById(id);
            if (informationFromDb == null)
                return new NotFoundResult();
            information.Id = id;
            await _informationService.Update(information);
            return new OkResult();
        }
        // DELETE: api/Informations/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var informationFromDb = await _informationService.GetById(id);
            if (informationFromDb == null)
                return new NotFoundResult();
            await _informationService.Delete(id);
            return new OkResult();
        }
    }
}