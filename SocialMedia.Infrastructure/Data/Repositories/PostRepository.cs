using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Data.Contexts;

namespace SocialMedia.Infrastructure.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        private readonly SocialMediaDbContext _context;

        public PostRepository(SocialMediaDbContext context)
        : base(context)
        {
            _context = context;
        }
        public override IReadOnlyList<Post> Get()
        {
            return _context.Posts.ToList();
        }

        public override Post Get(int id)
        {
            return _context.Posts.Include(p => p.Comments).FirstOrDefault(x => x.Id == id);
        }
        public override IReadOnlyList<Post> Filter(Func<Post, bool> predicate)
        {
            return _context.Posts.Where(predicate).ToList();
        }
    }
}
