using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Skill
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("n"), BsonRequired, Required]
        public string Name { get; set; }

        [BsonElement("tech")]
        public bool IsTechnical { get; set; } = true;

        [BsonElement("lvl"), Range(0, 5)]
        public int Level { get; set; } = 0;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update"), DataType(DataType.DateTime)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            Skill skill = (Skill)obj;
            return this.Name == skill.Name;
        }
    }
}
