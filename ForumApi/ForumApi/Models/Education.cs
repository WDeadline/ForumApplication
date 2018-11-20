using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Education
    {
        [BsonElement("maj")]
        public string Major { get; set; }
        [BsonElement("uni")]
        public string University { get; set; }
        [BsonElement("start")]
        public DateTime StartTime { get; set; }
        [BsonElement("end")]
        public DateTime EndTime { get; set; }
        [BsonElement("gpa")]
        public float GPA { get; set; }
    }
}
