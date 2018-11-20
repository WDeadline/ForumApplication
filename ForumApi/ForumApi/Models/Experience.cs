﻿using ForumApi.Validations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Experience
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("uid"), BsonRequired, StringLength(24, MinimumLength = 24)]
        public string UserId { get; set; }

        [BsonElement("wkpl"), BsonRequired]
        public string Workplace { get; set; }

        [BsonElement("pos"), BsonRequired]
        public string Position { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("start"), BsonRequired]
        public DateTime StartTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [DateGreaterThan("StartTime", ErrorMessage = "End date has to be later than start date")]
        [BsonElement("end"), BsonRequired]
        public DateTime EndTime { get; set; }

        [BsonElement("des"), BsonRequired]
        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("update")]
        public DateTime? UpdationTime { get; set; }
    }
}
