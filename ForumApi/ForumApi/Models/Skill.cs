using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Skill
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("uid"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string UserId { get; set; }

        [BsonElement("title"), BsonRequired, Required]
        public string Title { get; set; }

        [BsonElement("lvl"), Range(0, 5)]
        public int Level { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update")]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;

    }
}
