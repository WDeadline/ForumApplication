using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Interview
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("uid"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string UserId { get; set; }

        [BsonElement("cpnid"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string CompanyId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("cpnid"), BsonRequired, Required, DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; } = true;
    }
}
