using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Kontur.GameStats.Server.Data.Core.Repositories;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly GameStatsDbContext Db;
        private readonly DbSet<TEntity> _entities;

        public Repository(GameStatsDbContext db)
        {
            Db = db;
            _entities = Db.Set<TEntity>();
        }


        public IEnumerable<TEntity> All()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }
        
        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public void MarkAsModified(TEntity entity)
        {
            if (Db.Entry(entity).State != EntityState.Added)
                Db.Entry(entity).State = EntityState.Modified;
        }
    }
}
