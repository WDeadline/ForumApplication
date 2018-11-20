using ForumApi.Extensions;
using ForumApi.Validations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("fn"), BsonRequired, PersonName]
        public string FirstName { get; set; }

        [BsonElement("ln"), BsonRequired, PersonName]
        public string LastName { get; set; }

        [BsonElement("uid"), BsonRequired]
        public string Username { get; set; }

        [BsonElement("email"), BsonRequired]
        public string EmailAddress { get; set; }

        [BsonElement("avt")]
        public string Avatar { get; set; } = string.Empty;

        [JsonIgnore]
        [BsonElement("pwd"), BsonRequired]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        [BsonElement("salt"), BsonRequired]
        public byte[] PasswordSalt { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update")]
        public DateTime? UpdationTime { get; set; }

        [BsonElement("act")]
        public bool Active { get; set; } = true;

        [BsonElement("roles")]
        public IEnumerable<Role> Roles { get; set; }
    }
}
