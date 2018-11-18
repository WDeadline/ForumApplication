using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Avatar
    {
        [Required]
        public Picture Images { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
