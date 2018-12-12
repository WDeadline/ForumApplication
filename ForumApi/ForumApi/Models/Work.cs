using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Work
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cpnid"), BsonRequired, StringLength(24, MinimumLength = 24)]
        public string CompanyId { get; set; }

        [BsonElement("title"), BsonRequired, Required]
        public string Title { get; set; }

        [BsonElement("pos"), BsonRequired, Required]
        public string Position { get; set; }

        [BsonElement("des"), BsonRequired, Required]
        public string Description { get; set; }

        [BsonElement("addr"), BsonRequired, Required]
        public string Address { get; set; }

        [BsonElement("salary"), BsonRequired, Required]
        public string Salary { get; set; }

        [BsonElement("tags"), BsonRequired, Required]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update"), DataType(DataType.DateTime)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
    }
}
