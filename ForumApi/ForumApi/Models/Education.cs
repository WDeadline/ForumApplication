using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Education
    {
        [BsonElement("mj")]
        public string Major { get; set; }
        [BsonElement("unv")]
        public string University { get; set; }
        [BsonElement("stat")]
        public DateTime StartTime { get; set; }
        [BsonElement("endt")]
        public DateTime EndTime { get; set; }
        [BsonElement("gpa")]
        public float GPA { get; set; }
    }
}
