using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Models
{
    public class Comment
    {
        public string CommentBy { get; set; }
        public string Message { get; set; }
        public DateTime CreateOn { get; set; }
        public int Like { get; set; }
    }
}
