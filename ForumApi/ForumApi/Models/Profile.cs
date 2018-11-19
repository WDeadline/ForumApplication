using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Profile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("usr")]
        public string UserId { get; set; }
        [BsonElement("inf")]
        public Information Information { get; set; }
        [BsonElement("ojt")]
        public IEnumerable<Objective> Objectives { get; set; }
        [BsonElement("educ")]
        public IEnumerable<Education> Educations { get; set; }
        [BsonElement("exper")]
        public IEnumerable<Experience> Experiences { get; set; }
    }
}
