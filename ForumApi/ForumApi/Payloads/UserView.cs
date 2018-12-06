using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class UserView
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; } = string.Empty;
    }
}
