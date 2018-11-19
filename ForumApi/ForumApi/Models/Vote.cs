using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Vote
    {
        [BsonElement("vot")]
        public string VoteBy { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("cre")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
