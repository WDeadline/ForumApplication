using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Experience
    {
        [BsonElement("wpl")]
        public string Workplace { get; set; }
        [BsonElement("pos")]
        public string Position { get; set; }
        [BsonElement("stat")]
        public DateTime StartTime { get; set; }
        [BsonElement("endt")]
        public DateTime EndTime { get; set; }
        [BsonElement("des")]
        public string Description { get; set; }
    }
}
