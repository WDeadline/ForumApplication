using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Comment
    {
        [BsonElement("by")]
        public string CommentBy { get; set; }
        [BsonElement("cont")]
        public string Content { get; set; }
        [BsonElement("create")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
