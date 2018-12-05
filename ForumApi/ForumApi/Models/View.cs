using MongoDB.Bson;
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
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = new ObjectId().ToString();

        [BsonElement("by"), StringLength(24, MinimumLength = 24)]
        public string ViewBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("create"), DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
