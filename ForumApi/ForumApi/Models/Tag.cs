using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Tag
    {
        [BsonElement("desplay"), BsonRequired, Required]
        public string Desplay { get; set; }

        [BsonElement("val"), BsonRequired, Required]
        public string Value { get; set; }
    }
}
