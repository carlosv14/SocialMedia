using System;
using System.Collections.Generic;
using System.Linq;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Data.Contexts;

namespace SocialMedia.Infrastructure.Data.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private readonly SocialMediaDbContext _context;

        public CommentRepository(SocialMediaDbContext context)
        : base(context)
        {
            _context = context;
        }

        public override IReadOnlyList<Comment> Get()
        {
            return _context.Comments.ToList();
        }

        public override Comment Get(int id)
        {
            return _context.Comments.FirstOrDefault(x => x.Id == id);
        }

        public override IReadOnlyList<Comment> Filter(Func<Comment, bool> predicate)
        {
            return _context.Comments.Where(predicate).ToList();
        }
    }
}
