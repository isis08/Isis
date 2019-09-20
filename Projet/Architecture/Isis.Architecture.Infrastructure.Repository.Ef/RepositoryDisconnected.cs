using System;
using Isis.Architecture.Core.Domain.Entity;
using Isis.Architecture.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Isis.Architecture.Infrastructure.Repository.Ef
{
    public class RepositoryDisconnected<TEntity> : RepositoryConnected<TEntity>, IRepositoryDisconnected<TEntity> where TEntity : EntityBase, IState
    {
        public RepositoryDisconnected(DbContext context) : base(context)
        {
        }

        public void CreateOrUpdate(TEntity entity)
        {
            ApplyGraphChanges(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            context.Attach(entity);
            ApplyGraphChanges(entity);
        }
        public virtual void Detach(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            context.Entry(entity).State = EntityState.Detached;
            ApplyGraphChanges(entity);
        }

        private EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added: return EntityState.Added;
                case State.Deleted: return EntityState.Deleted;
                case State.Modified: return EntityState.Modified;
                default: return EntityState.Unchanged;
            }
        }

        private void ApplyGraphChanges(TEntity racine)
        {
            context.ChangeTracker.TrackGraph(racine, TrackGraph);
        }

        private void TrackGraph(EntityEntryGraphNode e)
        {
            if (!(e.Entry.Entity is IState entityWithState))
                throw new Exception($"Entity {e.GetType()} must implement {typeof(IState)}");

            e.Entry.State = ConvertState(entityWithState.State);
        }


    }
}
