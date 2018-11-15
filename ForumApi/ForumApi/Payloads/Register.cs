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

        [Required, MaxLength(25)]
        [RegularExpression(RegexText.VIETNAMESEREGEX, ErrorMessage = "Sorry, The first name should not contain any special characters.")]
        public string FirstName { get; set; }

        [Required, MaxLength(25)]
        [RegularExpression(RegexText.VIETNAMESEREGEX, ErrorMessage = "Sorry, The last should not contain any special characters.")]
        public string LastName { get; set; }

        [Required, MaxLength(20)]
        [RegularExpression(RegexText.USERNAMEREGEX, ErrorMessage = "The Username can only contain alphanumeric characters (letters a-zA-Z, numbers 0-9).")]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
