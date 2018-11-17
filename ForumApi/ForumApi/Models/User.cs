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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreationTime { get; set; }
        /*public DateTime CreationTime {
            get
            {
                string id = null;
                if(ObjectId.TryParse(id, out ObjectId objectId))
                {
                    return objectId.CreationTime;
                }
                return new DateTime();
            }
        }*/
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public IEnumerable<string> Roles { get; set; }
    }
}
