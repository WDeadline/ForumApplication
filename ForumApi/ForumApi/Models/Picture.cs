using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Picture
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name"), BsonRequired]
        public string Name { get; set; }

        [BsonElement("path"), BsonRequired]
        public string Path { get; set; }

        [BsonElement("len")]
        public int Length { get; set; }

        [BsonElement("wid")]
        public int Width { get; set; }

        [BsonElement("hgt")]
        public int Height { get; set; }

        [BsonElement("type"), BsonRequired]
        public string ContentType { get; set; }
    }
}
