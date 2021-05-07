namespace SocialMedia.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }

        //navigation property
        public Post Post { get; set; }
    }
}
