using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Information
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("uid"), BsonRequired, Required, StringLength(24, MinimumLength = 24)]
        public string UserId { get; set; }

        public string FullName { get; set; }

        [BsonElement("gen")]
        public bool? Gender { get; set; }

        [BsonElement("dob")]
        public DateTime? DateOfBirth { get; set; }

        [Phone]
        [BsonElement("phone")]
        public string PhoneNumber { get; set; }

        [BsonElement("addr")]
        public string Address { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update")]
        public DateTime? UpdationTime { get; set; }
    }
}
