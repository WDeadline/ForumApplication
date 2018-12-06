using ForumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApi.Payloads
{
    public class QuestionResponse
    {
        public string Id { get; set; }

        public UserView UserView { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

        public ICollection<Report> Reports { get; set; } = new List<Report>();

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public ICollection<View> Views { get; set; } = new List<View>();

        public DateTime UpdationTime { get; set; } = DateTime.UtcNow;
    }
}
