using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class Register
    {
        [Required]
        public string Name { get; set; }

        [Required, RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]{3,30}$"), StringLength(30)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
