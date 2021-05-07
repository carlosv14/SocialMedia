using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        IReadOnlyList<TEntity> Get();

        TEntity Get(int id);

        TEntity Create(TEntity entity);

        IReadOnlyList<TEntity> Filter(Func<TEntity, bool> predicate);
    }
}
