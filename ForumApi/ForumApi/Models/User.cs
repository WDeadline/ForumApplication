using ForumApi.Extensions;
using ForumApi.Validations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("n"), BsonRequired, Required, PersonName, MaxLength(200)]
        public string Name { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("bd"), DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [BsonElement("phone")]
        public string PhoneNumber { get; set; }

        [BsonElement("addr")]
        public string Address { get; set; }

        [BsonElement("pos")]
        public string Position { get; set; }

        [BsonElement("uid"), BsonRequired, Required]
        public string Username { get; set; }

        [BsonElement("email"), BsonRequired, Required]
        public string EmailAddress { get; set; }

        [JsonIgnore]
        [BsonElement("pwd"), BsonRequired, Required]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        [BsonElement("salt"), BsonRequired, Required]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("avt")]
        public string Avatar { get; set; } = string.Empty;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update"), DataType(DataType.DateTime)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;

        [BsonElement("active")]
        public bool Active { get; set; } = true;

        [BsonElement("roles")]
        public IEnumerable<Role> Roles { get; set; } = new List<Role> { Role.Student };

        [BsonElement("objectives")]
        public ICollection<Objective> Objectives { get; set; }

        [BsonElement("educations")]
        public ICollection<Education> Educations { get; set; }

        [BsonElement("skills")]
        public ICollection<Skill> Skills { get; set; }

        [BsonElement("experiences")]
        public ICollection<Experience> Experiences { get; set; }

        [BsonElement("activities")]
        public ICollection<Activity> Activities { get; set; }
    }
}
