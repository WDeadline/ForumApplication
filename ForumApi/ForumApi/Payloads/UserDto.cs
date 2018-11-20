using ForumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public string Token { get; set; }
    }
}
