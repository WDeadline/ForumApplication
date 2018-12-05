using ForumApi.Models;
using ForumApi.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class CreationUser
    {
        [Required, PersonName, MaxLength(200)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Position { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public IEnumerable<Role> Roles { get; set; }

    }
}
