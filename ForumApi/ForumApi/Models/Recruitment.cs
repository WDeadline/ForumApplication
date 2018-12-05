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
    public class Recruitment
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cpnid"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string CompanyId { get; set; }

        [BsonElement("title"), BsonRequired, Required]
        public string Title { get; set; }

        [BsonElement("place"), BsonRequired, Required]
        public string Place { get; set; }

        [BsonElement("des"), BsonRequired, Required]
        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("start"), BsonRequired, Required, DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DateGreaterThan("StartTime", ErrorMessage = "End date has to be later than start date")]
        [BsonElement("end"), BsonRequired, Required, DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [BsonElement("tags"), BsonRequired, Required]
        public ICollection<Tag> Tags { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update"), DataType(DataType.DateTime)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
    }
}
