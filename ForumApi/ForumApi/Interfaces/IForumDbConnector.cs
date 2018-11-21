using ForumApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Interfaces
{
    public interface IForumDbConnector
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Question> Questions { get; }
        IMongoCollection<Education> Educations { get; }
        IMongoCollection<Experience> Experiences { get; }
        IMongoCollection<Objective> Objectives { get; }
        IMongoCollection<Information> Informations { get; }
    }
}
