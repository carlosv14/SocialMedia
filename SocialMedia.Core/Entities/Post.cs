using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
