using ForumApi.Validations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Education
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("maj"), BsonRequired, Required]
        public string Major { get; set; }

        [BsonElement("univ"), BsonRequired, Required]
        public string University { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("start"), BsonRequired, Required, DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DateGreaterThan("StartTime", ErrorMessage = "End date has to be later than start date")]
        [BsonElement("end"), BsonRequired, Required, DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [BsonElement("gpa"), BsonRequired, Required, Range(0, 4)]
        public float GPA { get; set; } = 0;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update"), DataType(DataType.DateTime)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
    }
}
