using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("tle")]
        public string Title { get; set; }
        [BsonElement("des")]
        public string Description { get; set; }
        [BsonElement("tags")]
        public ICollection<string> Tags { get; set; }
        [BsonElement("vies")]
        public IEnumerable<View> Views { get; set; }
        [BsonElement("vts")]
        public IEnumerable<Vote> Votes { get; set; }
        [BsonElement("anss")]
        public IEnumerable<Answer> Answers { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreationTime
        {
            get
            {
                if (ObjectId.TryParse(this.Id, out ObjectId objectId))
                {
                    return objectId.CreationTime;
                }
                return new DateTime();
            }
        }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("udt")]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
    }
}
