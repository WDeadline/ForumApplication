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
        [BsonElement("cmtb")]
        public string CommentBy { get; set; }
        [BsonElement("cont")]
        public string Content { get; set; }
        [BsonElement("cret")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
