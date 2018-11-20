using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class View
    {
        [BsonElement("by"), BsonRequired, StringLength(24, MinimumLength = 24)]
        public string ViewBy { get; set; }

        [BsonElement("create")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
