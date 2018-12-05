﻿using ForumApi.Interfaces;
using ForumApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Services
{
    public class QuestionService : IService<Question>
    {
        private readonly ILogger<QuestionService> _logger;
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(ILogger<QuestionService> logger, IRepository<Question> questionRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;

        }

        public Task Add(Question entity)
        {
            try
            {
                return _questionRepository.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                return _questionRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }

        public Task<IEnumerable<Question>> GetAll()
        {
            try
            {
                return _questionRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            try
            {
                IList<Tag> tags = new List<Tag>();
                foreach(var question in await _questionRepository.GetAll())
                {
                    foreach(var tag in question.Tags)
                    {
                        if (tags.IndexOf(tag) == -1) {
                            tags.Add(tag);
                        }
                    }
                    
                }
                return tags;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public Task<Question> GetById(string id)
        {
            try
            {
                return _questionRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, id);
                throw ex;
            }
        }


        public Task<bool> Update(Question entity)
        {
            try
            {
                entity.UpdationTime = DateTime.UtcNow;
                return _questionRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, entity);
                throw ex;
            }
        }
    }
}
