using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ForumApi.Interfaces;
using ForumApi.Models;
using ForumApi.Payloads;
using ForumApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IService<Question> _questionService;
        private readonly IUserService _userService;
        public QuestionsController(ILogger<QuestionsController> logger, IService<Question> questionService, IUserService userService)
        {
            _logger = logger;
            _questionService = questionService;
            _userService = userService;
        }

        // GET: api/questions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<QuestionResponse> questionResponses = new List<QuestionResponse>();
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            var questions = await _questionService.GetAll();
            foreach(var question in questions)
            {
                var userFromDb = await _userService.GetById(question.QuestionBy);
                userFromDb.Avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "";
                QuestionResponse questionResponse = new QuestionResponse
                {
                    Id = question.Id,
                    UserView = new UserView
                    {
                        Id = userFromDb.Id,
                        Name = userFromDb.Name,
                        Avatar = userFromDb.Avatar,
                    },
                    Title = question.Title,
                    Description = question.Description,
                    Tags = question.Tags,
                    Votes = question.Votes,
                    Views = question.Views,
                    Reports = question.Reports,
                    Answers = question.Answers,
                    UpdationTime = question.UpdationTime
                };
                questionResponses.Add(questionResponse);
            }
            return new OkObjectResult(questionResponses);
        }

        // GET: api/questions/id
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
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
            var userFromDb = await _userService.GetById(question.QuestionBy);
            userFromDb.Avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "";
            QuestionResponse questionResponse = new QuestionResponse
            {
                Id = question.Id,
                UserView = new UserView
                {
                    Id = userFromDb.Id,
                    Name = userFromDb.Name,
                    Avatar = userFromDb.Avatar,
                },
                Title = question.Title,
                Description = question.Description,
                Tags = question.Tags,
                Votes = question.Votes,
                Views = question.Views,
                Reports = question.Reports,
                Answers = question.Answers,
                UpdationTime = question.UpdationTime
            };
            return new OkObjectResult(questionResponse);
        }

        // POST: api/questions/id/vote
        [HttpPost("{id:length(24)}/vote")]
        public async Task<IActionResult> PostVote(string id)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

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
            IEnumerable<Tag> tag = await ((QuestionService)_questionService).GetAllTagsAsync();
            return new OkObjectResult(tag);
        }

        [Authorize]
        // POST: api/questions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]QuestionRequest questionRequest)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userFromDb = await _userService.GetById(userId);
            userFromDb.Avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "";
            Question question = new Question
            {
                QuestionBy = userId,
                Title = questionRequest.Title,
                Description = questionRequest.Description,
                Tags = questionRequest.Tags,
            };

            await _questionService.Add(question);
            QuestionResponse questionResponse = new QuestionResponse
            {
                Id = question.Id,
                UserView = new UserView
                {
                    Id = userFromDb.Id,
                    Name = userFromDb.Name,
                    Avatar = userFromDb.Avatar,
                },
                Title = question.Title,
                Description = question.Description,
                Tags = question.Tags,
                Votes = question.Votes,
                Views = question.Views,
                Reports = question.Reports,
                Answers = question.Answers,
                UpdationTime = question.UpdationTime
            };
            return new OkObjectResult(questionResponse);
        }
        [Authorize]
        // PUT: api/questions/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, [FromBody]QuestionRequest questionRequest)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!(questionFromDb.QuestionBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            questionFromDb.Title = questionRequest.Title;
            questionFromDb.Description = questionRequest.Description;
            questionFromDb.Tags = questionRequest.Tags;
            questionFromDb.UpdationTime = DateTime.UtcNow;

            await _questionService.Update(questionFromDb);
            return new OkResult();
        }
        [Authorize]
        // DELETE: api/questions/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!(questionFromDb.QuestionBy.Equals(userId) 
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            await _questionService.Delete(id);
            return new OkResult();
        }

        #region answer
        // GET: api/questions/questionId/answers
        [HttpGet("{id:length(24)}/answers")]
        public async Task<IActionResult> GetAnswers(string id)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            return new OkObjectResult(questionFromDb.Answers);
        }

        [Authorize]
        // POST: api/questions/questionId/answers
        [HttpPost("{id:length(24)}/answers")]
        public async Task<IActionResult> PostAnswer(string id, [FromBody]AnswerRequest answerRequest)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Answer answer = new Answer
            {
                Content = answerRequest.Content
            };
            questionFromDb.Answers.Add(answer);
            _questionService.Update(questionFromDb);

            return new OkObjectResult(answer);
        }

        [Authorize]
        // PUT: api/questions/questionId/answers/answerId 
        [HttpPut("{id:length(24)}/answers/{answerId:length(24)}")]
        public async Task<IActionResult> PutAnswer(string id, string answerId, [FromBody]AnswerRequest answerRequest)
        {
            
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Answer questionAnswer = questionFromDb.Answers.FirstOrDefault(s => s.Id == answerId);
            if (questionAnswer == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (!(questionFromDb.QuestionBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            questionAnswer.Content = answerRequest.Content;
            questionAnswer.UpdationTime = DateTime.UtcNow;
            _questionService.Update(questionFromDb);
            return new OkObjectResult(questionAnswer);
        }

        [Authorize]
        // PUT: api/questions/questionId/answers/answerId  
        [HttpDelete("{id:length(24)}/answers/{answerId:length(24)}")]
        public async Task<IActionResult> DeleteAnswer(string id, string answerId)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Answer questionAnswer = questionFromDb.Answers.FirstOrDefault(s => s.Id == answerId);
            if (questionAnswer == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (!(questionFromDb.QuestionBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            questionFromDb.Answers.Remove(questionAnswer);
            _questionService.Update(questionFromDb);
            return new OkResult();
        }
        #endregion

    }
}
