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
            IList<object> questionResponses = new List<object>();
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            var questions = await _questionService.GetAll();
            
            foreach(var question in questions)
            {
                questionResponses.Add(GetQuestionView(question).Result);
            }
            return new OkObjectResult(questionResponses);
        }

        #region getView
        public async Task<object> GetQuestionView(Question question)
        {
            var userFromDb = await _userService.GetById(question.QuestionBy);
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            userFromDb.Avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "";
            var questionView = new
            {
                id = question.Id,
                questionBy = question.QuestionBy,
                userView = new
                {
                    id = userFromDb.Id,
                    name = userFromDb.Name,
                    avatar = userFromDb.Avatar,
                },
                title = question.Title,
                description = question.Description,
                tags = question.Tags,
                votes = GetVotesView(question.Votes).Result,
                views = GetViewsView(question.Views).Result,
                reports = GetReportsView(question.Reports).Result,
                answers = GetAnswersView(question.Answers).Result,
                updationTime = question.UpdationTime
            };
            return questionView;
        }

        public async Task<List<object>> GetVotesView(ICollection<Vote> votes)
        {
                string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
                List<object> votesView = new List<object>();

                foreach (var vote in votes)
                {
                    votesView.Add(GetVoteView(vote).Result);
                }
                return votesView;
        }

        public async Task<object> GetVoteView(Vote vote)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            var userFromDb = await _userService.GetById(vote.VoteBy);
            object voteView = new
            {
                id = vote.Id,
                voteBy = vote.VoteBy,
                userView = new
                {
                    id = userFromDb.Id,
                    name = userFromDb.Name,
                    avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                },
                creationTime = vote.CreationTime
            };
            return voteView;
        }


        public async Task<List<object>> GetViewsView(ICollection<View> views)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
            List<object> viewsView = new List<object>();
            foreach (var view in views)
            {
                viewsView.Add(GetViewView(view).Result);
            }
            return viewsView;
        }

        public async Task<object> GetViewView(View view)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
                var userFromDb = await _userService.GetById(view.ViewBy);
                object viewView = new
                {
                    id = view.Id,
                    viewBy = view.ViewBy,
                    userView = new
                    {
                        id = userFromDb.Id,
                        name = userFromDb.Name,
                        avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                    },
                    creationTime = view.CreationTime
                };
            return viewView;
        }

        public async Task<List<object>> GetReportsView(ICollection<Report> reports)
        {
            List<object> viewsView = new List<object>();
            foreach (var report in reports)
            {
                viewsView.Add(GetReportView(report).Result);
            }
            return viewsView;
        }

        public async Task<object> GetReportView(Report report)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
                var userFromDb = await _userService.GetById(report.ReportBy);
                object reportView = new
                {
                    id = report.Id,
                    reportBy = report.ReportBy,
                    UserView = new
                    {
                        id = userFromDb.Id,
                        name = userFromDb.Name,
                        avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                    },
                    description = report.Description,
                    creationTime = report.CreationTime
                };
            return reportView;
        }


        public async Task<List<object>> GetAnswersView(ICollection<Answer> answers)
        {
            List<object> viewsView = new List<object>();
            foreach (var answer in answers)
            {
                viewsView.Add(GetAnswerView(answer).Result);
            }
            return viewsView;
        }

        public async Task<object> GetAnswerView(Answer answer)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
                var userFromDb = await _userService.GetById(answer.AnswerBy);
                object answerView = new
                {
                    id = answer.Id,
                    answerBy = answer.AnswerBy,
                    UserView = new
                    {
                        id = userFromDb.Id,
                        name = userFromDb.Name,
                        avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                    },
                    content = answer.Content,
                    votes = GetVotesView(answer.Votes).Result,
                    comments = GetCommentsView(answer.Comments).Result,
                    updationTime = answer.UpdationTime
                };
            
            return answerView;
        }

        public async Task<List<object>> GetCommentsView(ICollection<Comment> comments)
        {
            List<object> viewsView = new List<object>();
            foreach (var comment in comments)
            {
                viewsView.Add(GetCommentView(comment).Result);
            }
            return viewsView;
        }

        public async Task<object> GetCommentView(Comment comment)
        {
            string path = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.ToString(), "/images/avatars/");
                var userFromDb = await _userService.GetById(comment.CommentBy);
                object commentView = new
                {
                    id = comment.Id,
                    commentBy = comment.CommentBy,
                    UserView = new
                    {
                        id = userFromDb.Id,
                        name = userFromDb.Name,
                        avatar = !string.IsNullOrEmpty(userFromDb.Avatar) ? path + userFromDb.Avatar : "",
                    },
                    content = comment.Content,
                    updationTime = comment.UpdationTime
                };
            return commentView;
        }
        #endregion

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
                    Id = ObjectId.GenerateNewId().ToString(),
                    ViewBy = userId
                };
                question.Views.Add(view);
                _questionService.Update(question);
            }
            return new OkObjectResult(GetQuestionView(question).Result);
        }

        [Authorize]
        // POST: api/questions/id/vote
        [HttpPost("{id:length(24)}/votes")]
        public async Task<IActionResult> PostVote(string id)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
            return new NotFoundResult();
            Vote vote = questionFromDb.Votes.FirstOrDefault(v => v.VoteBy == userId);
            if(vote == null)
            {
                vote = new Vote
                {
                    VoteBy = userId,
                };
                questionFromDb.Votes.Add(vote);
                _questionService.Update(questionFromDb);
            }
            
            return new OkObjectResult(GetVoteView(vote).Result);
        }

        [Authorize]
        // DELETE: api/questions/id/vote
        [HttpDelete("{id:length(24)}/votes")]
        public async Task<IActionResult> DeteleVote(string id)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Vote vote = questionFromDb.Votes.FirstOrDefault(v => v.VoteBy == userId);
            if(vote != null)
            {
                questionFromDb.Votes.Remove(vote);
                _questionService.Update(questionFromDb);
            }
            return new OkResult();
        }

        [Authorize]
        // POST: api/questions/id/vote
        [HttpPost("{id:length(24)}/answer/{answerId:length(24)}/votes")]
        public async Task<IActionResult> PostAnswerVote(string id, string answerId)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Answer answer = questionFromDb.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
                return new NotFoundResult();
            Vote vote = answer.Votes.FirstOrDefault(v => v.VoteBy == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    VoteBy = userId,
                };
                answer.Votes.Add(vote);
                _questionService.Update(questionFromDb);
            }

            return new OkObjectResult(GetVoteView(vote).Result);
        }

        [Authorize]
        // DELETE: api/questions/id/vote
        [HttpDelete("{id:length(24)}/answer/{answerId:length(24)}/votes")]
        public async Task<IActionResult> DeteleAnswerVote(string id, string answerId)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            Answer answer = questionFromDb.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
                return new NotFoundResult();
            Vote vote = answer.Votes.FirstOrDefault(v => v.VoteBy == userId);
            if (vote != null)
            {
                questionFromDb.Votes.Remove(vote);
                _questionService.Update(questionFromDb);
            }
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
            return new OkObjectResult(GetQuestionView(question).Result);
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
            return new OkObjectResult(GetAnswersView(questionFromDb.Answers).Result);
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
                AnswerBy = userId,
                Content = answerRequest.Content
            };
            questionFromDb.Answers.Add(answer);
            _questionService.Update(questionFromDb);

            return new OkObjectResult(GetAnswerView(answer).Result);
        }

        [Authorize]
        // PUT: api/questions/questionId/answers/answerId 
        [HttpPut("{id:length(24)}/answers/{answerId:length(24)}")]
        public async Task<IActionResult> PutAnswer(string id, string answerId, [FromBody]AnswerRequest answerRequest)
        {
            
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            var answer = questionFromDb.Answers.FirstOrDefault(s => s.Id == answerId);
            if (answer == null)
                return new NotFoundResult();
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (!(answer.AnswerBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            answer.Content = answerRequest.Content;
            answer.UpdationTime = DateTime.UtcNow;
            _questionService.Update(questionFromDb);
            return new OkObjectResult(GetAnswerView(answer).Result);
        }

        [Authorize]
        // PUT: api/questions/questionId/answers/answerId  
        [HttpDelete("{id:length(24)}/answers/{answerId:length(24)}")]
        public async Task<IActionResult> DeleteAnswer(string id, string answerId)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            var answer = questionFromDb.Answers.FirstOrDefault(s => s.Id == answerId);
            if (answer == null)
                return new NotFoundResult();

            if (!(answer.AnswerBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            questionFromDb.Answers.Remove(answer);
            _questionService.Update(questionFromDb);
            return new OkResult();
        }
        #endregion

        #region answer
        // GET: api/questions/questionId/answers/answerId/comments
        [HttpGet("{id:length(24)}/answers/{answerId:length(24)}/comments")]
        public async Task<IActionResult> GetComments(string id, string answerId)
        {
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            var answer = questionFromDb.Answers.FirstOrDefault(a => a.Id == answerId);
            if(answer == null)
                return new NotFoundResult();

            return new OkObjectResult(GetCommentsView(answer.Comments).Result);
        }

        [Authorize]
        // POST: api/questions/questionId/answers/answerId.comments
        [HttpPost("{id:length(24)}/answers/{answerId:length(24)}/comments")]
        public async Task<IActionResult> PostComments(string id, string answerId, [FromBody]CommentRequest commentRequest)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            var answer = questionFromDb.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
                return new NotFoundResult();
            Comment comment = new Comment
            {
                CommentBy = userId,
                Content = commentRequest.Content
            };
            answer.Comments.Add(comment);
            _questionService.Update(questionFromDb);

            return new OkObjectResult(GetCommentView(comment).Result);
        }

        [Authorize]
        // PUT: api/questions/questionId/answers/answerId/comments/commentId
        [HttpPut("{id:length(24)}/answers/{answerId:length(24)}/comments/{commentId:length(24)}")]
        public async Task<IActionResult> PutComment(string id, string answerId, string commentId, [FromBody]CommentRequest commentRequest)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            var answer = questionFromDb.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
                return new NotFoundResult();
            var comment = answer.Comments.FirstOrDefault(c => c.Id == commentId);
            if(comment == null)
                return new NotFoundResult();
            if (!(comment.CommentBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }

            comment.Content = commentRequest.Content;
            comment.UpdationTime = DateTime.UtcNow;
            _questionService.Update(questionFromDb);
            return new OkObjectResult(GetCommentView(comment).Result);
        }

        [Authorize]
        // PUT: api/questions/questionId/answers/answerId/comments/commentId
        [HttpDelete("{id:length(24)}/answers/{answerId:length(24)}/comments/{commentId:length(24)}")]
        public async Task<IActionResult> DeleteAnswer(string id, string answerId, string commentId)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var questionFromDb = await _questionService.GetById(id);
            if (questionFromDb == null)
                return new NotFoundResult();
            var answer = questionFromDb.Answers.FirstOrDefault(a => a.Id == answerId);
            if (answer == null)
                return new NotFoundResult();
            var comment = answer.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                return new NotFoundResult();
            if (!(comment.CommentBy.Equals(userId)
                || HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Administrator")))
            {
                return Forbid();
            }
            answer.Comments.Remove(comment);
            _questionService.Update(questionFromDb);
            return new OkResult();
        }
        #endregion
    }
}
