using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Vote
    {
        [BsonElement("by"), BsonRequired, StringLength(24, MinimumLength = 24)]
        public string VoteBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("create")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
