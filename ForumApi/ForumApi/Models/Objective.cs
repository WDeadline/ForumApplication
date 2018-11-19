using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Objective
    {
        [BsonElement("des")]
        public string Description { get; set; }
    }
}
