using ForumApi.Validations;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class UpdationUser
    {
        [Required, PersonName, MaxLength(200)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }
    }
}
