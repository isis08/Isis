using System.Collections.Generic;
using Isis.Architecture.Core.Domain.Entity;
using Isis.Architecture.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Isis.Architecture.Infrastructure.Repository.Ef
{
    public class RepositoryConnected<TEntity> : RepositoryBase<TEntity>, IRepositoryConnected<TEntity> where TEntity : EntityBase
    {
        public RepositoryConnected(DbContext context) : base(context)
        {
        }

        public virtual void Add(TEntity entity)
        {
            entitySet.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            entitySet.AddRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            entitySet.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            entitySet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            entitySet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            entitySet.UpdateRange(entities);
        }

    }
}
