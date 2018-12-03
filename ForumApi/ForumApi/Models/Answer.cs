using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Answer
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = new ObjectId().ToString();

        [BsonElement("by"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string AnswerBy { get; set; }

        [BsonElement("cont"), BsonRequired, Required]
        public string Content { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update"), DataType(DataType.DateTime)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;

        [BsonElement("votes")]
        public ICollection<Vote> Votes { get; set; }

        [BsonElement("cmts")]
        public IEnumerable<Comment> Comments { get; set; }
    }
}
