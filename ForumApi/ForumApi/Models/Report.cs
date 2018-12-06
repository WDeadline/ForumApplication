using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Report
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("by"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string ReportBy { get; set; }

        [BsonElement("des"), BsonRequired, Required]
        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("create"), DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
