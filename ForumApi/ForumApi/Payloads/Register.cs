using ForumApi.Commons;
using ForumApi.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class Register
    {
        [Required, MaxLength(25), PersonName]
        public string FirstName { get; set; }

        [Required, MaxLength(25), PersonName]
        public string LastName { get; set; }

        [Required, MaxLength(20), Username]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
