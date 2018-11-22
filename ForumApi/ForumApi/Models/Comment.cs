using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Comment
    {
        [BsonElement("by"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string CommentBy { get; set; }

        [BsonElement("cont"), BsonRequired, Required]
        public string Content { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("create")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("create")]
        public DateTime? UpdationTime { get; set; }
    }
}
