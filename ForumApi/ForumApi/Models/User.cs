using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreationTime { get; } = DateTime.Now;
        [BsonDateTimeOptions]
        public DateTime UpdationTime { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public IEnumerable<string> Roles { get; set; }
    }
}
