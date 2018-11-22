using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using ForumApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IService<Question> _questionService;
        public QuestionsController(ILogger<QuestionsController> logger, IService<Question> questionService)
        {
            _logger = logger;
            _questionService = questionService;
        }

        // GET: api/questions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _questionService.GetAll());
        }

        // GET: api/questions/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            string userId = HttpContext.User?.Identity.Name;

            var question = await _questionService.GetById(id);
            if (question == null)
                return new NotFoundResult();
            if(!string.IsNullOrEmpty(userId) && !question.Views.Any(v => v.ViewBy == userId))
            {
                View view = new View
                {
                    ViewBy = userId
                };
                question.Views.Append(view);
                _questionService.Update(question);
            }
            return new OkObjectResult(question);
        }

        // POST: api/questions/id/vote
        [HttpPost("{id:length(24)}/vote")]
        public async Task<IActionResult> PostVote(string id)
        {
            string userId = HttpContext.User?.Identity.Name;

            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            if (!string.IsNullOrEmpty(userId) && !questionFromDb.Votes.Any(v => v.VoteBy == userId))
            {
                Vote vote = new Vote
                {
                    VoteBy = userId,
                };
                questionFromDb.Votes.Append(vote);
                _questionService.Update(questionFromDb);
            }
            return new OkResult();
        }

        // POST: api/questions/id/vote
        [HttpDelete("{id:length(24)}/vote")]
        public async Task<IActionResult> DeteleVote(string id)
        {
            string userId = HttpContext.User?.Identity.Name;

            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Vote vote = questionFromDb.Votes.FirstOrDefault(v => v.VoteBy == userId);
            questionFromDb.Votes.Remove(vote);
            _questionService.Update(questionFromDb);
            return new OkResult();
        }

        // POST: api/questions/tags
        [HttpGet, Route("tags")]
        public async Task<IActionResult> GetTags([FromQuery(Name = "input")] string input)
        {
            IEnumerable<string> tag = await ((QuestionService)_questionService).GetAllTagsAsync();
            return new OkObjectResult(tag);
        }


        // POST: api/questions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
            await _questionService.Add(question);
            return new OkObjectResult(question);
        }
        // PUT: api/questions/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]Question question)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            question.Id = id;
            await _questionService.Update(question);
            return new OkResult();
        }
        // DELETE: api/questions/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            await _questionService.Delete(id);
            return new OkResult();
        }
    }
}
