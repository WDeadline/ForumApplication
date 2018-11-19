using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class View
    {
        [BsonElement("vie")]
        public string ViewBy { get; set; }
        [BsonElement("cre")]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
