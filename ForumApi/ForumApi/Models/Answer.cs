using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Answer
    {
        [BsonElement("ansb")]
        public string AnswerBy { get; set; }
        [BsonElement("cont")]
        public string Content { get; set; }
        [BsonElement("cret")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        [BsonElement("vts")]
        public IEnumerable<Vote> Votes { get; set; }
        [BsonElement("cmts")]
        public IEnumerable<Comment> Comments { get; set; }
    }
}
