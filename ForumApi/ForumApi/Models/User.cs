using ForumApi.Extensions;
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

        public bool Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [JsonIgnore]
        public IEnumerable<Avatar> Avatars { get; set; }

        public Avatar Avatar {
            get
            {
                if (!Avatars.IsNullOrEmpty())
                {
                    return Avatars.FirstOrDefault();
                }
                return null;
            }
        }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        [JsonIgnore]
        public IEnumerable<Password> Passwords { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreationTime {
            get
            {
                if(ObjectId.TryParse(this.Id, out ObjectId objectId))
                {
                    return objectId.CreationTime;
                }
                return new DateTime();
            }
        }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;

        public bool Active { get; set; } = true;

        public IEnumerable<string> Roles { get; set; }
    }
}
