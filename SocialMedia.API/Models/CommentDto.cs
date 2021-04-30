using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.API.Models
{
    public class CommentDto
    {
        public int CommentId { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }
    }
}
