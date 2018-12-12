using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class CommentRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
