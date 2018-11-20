﻿using ForumApi.Extensions;
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

        [BsonElement("avt")]
        public string Avatar { get; set; } = string.Empty;

        [BsonElement("uid")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string EmailAddress { get; set; }

        [JsonIgnore]
        [BsonElement("pwd")]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        [BsonElement("salt")]
        public byte[] PasswordSalt { get; set; }

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
        [BsonElement("update")]
        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;

        [BsonElement("act")]
        public bool Active { get; set; } = true;

        [BsonElement("roles")]
        public IEnumerable<Role> Roles { get; set; }
    }
}
