using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Information
    {
        [BsonElement("fn")]
        public string FirstName { get; set; }

        [BsonElement("ln")]
        public string LastName { get; set; }

        [BsonElement("gen")]
        public bool? Gender { get; set; }

        [BsonElement("dob")]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("phone")]
        public string PhoneNumber { get; set; }

        [BsonElement("addr")]
        public string Address { get; set; }
    }
}
