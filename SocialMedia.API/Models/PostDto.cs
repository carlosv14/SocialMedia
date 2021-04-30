using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.API.Models
{
    public class PostDto
    {
        public int PostId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
}
