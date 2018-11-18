using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Class
    {
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Workplace { get; set; } = string.Empty;

        public string AdvancedSkill { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Langage { get; set; } = string.Empty;
        //city / province
        //
    }
}
