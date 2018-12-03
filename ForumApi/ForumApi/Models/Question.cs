using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("uid"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string QuestionBy { get; set; }

        [BsonElement("title"), BsonRequired, Required]
        public string Title { get; set; }

        [BsonElement("des"), BsonRequired, Required]
        public string Description { get; set; }

        [BsonElement("tags"), BsonRequired, Required]
        public ICollection<Tag> Tags { get; set; }

        [BsonElement("votes")]
        public ICollection<Vote> Votes { get; set; }

        [BsonElement("reports")]
        public ICollection<Report> Reports { get; set; }

        [BsonElement("ans")]
        public ICollection<Answer> Answers { get; set; }

        [BsonElement("views")]
        public ICollection<View> Views { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update")]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
    }
}
