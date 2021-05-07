using System;
using System.Collections.Generic;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data.Contexts;

namespace SocialMedia.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    {
        private readonly SocialMediaDbContext _context;

        protected BaseRepository(SocialMediaDbContext context)
        {
            _context = context;
        }

        public abstract IReadOnlyList<TEntity> Get();

        public abstract TEntity Get(int id);

        public TEntity Create(TEntity entity)
        {
            _context.AddAsync(entity);
            _context.SaveChangesAsync();
            return entity;
        }

        public abstract IReadOnlyList<TEntity> Filter(Func<TEntity, bool> predicate);
    }
}
