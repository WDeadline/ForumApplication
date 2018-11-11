using ForumApi.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class Register
    {

        [Required, MaxLength(25), RegularExpression(RegexText.VIETNAMESEREGEX)]
        public string FirstName { get; set; }

        [Required, MaxLength(25), RegularExpression(RegexText.VIETNAMESEREGEX)]
        public string LastName { get; set; }

        [Required, StringLength(30), RegularExpression(RegexText.USERNAMEREGEX)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
