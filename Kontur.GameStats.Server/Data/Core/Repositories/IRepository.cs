using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void MarkAsModified(TEntity entity);
    }
}
