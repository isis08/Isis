using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Isis.Architecture.Core.Domain.Entity;
using Isis.Architecture.Core.Domain.Repository;
using Isis.Architecture.Core.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace Isis.Architecture.Infrastructure.Repository.Ef
{
    /// <summary>
    /// Generic repository implementation for EF Core.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbSet<TEntity> entitySet;
        protected readonly DbContext context;

        public RepositoryBase(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context)); ;
            this.entitySet = this.context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(entitySet));
        }

        public virtual IEnumerable<TEntity> All(IAggregatSpecification<TEntity> treeSpecification = null)
        {
            if (treeSpecification == null)
            {
                return entitySet;
            }

            return BuildAggregate(treeSpecification.IncludeLeafs, treeSpecification.IncludeTrees)
                .AsEnumerable();
        }

        public virtual Task<TEntity> GetAsync(long id)
        {
            return entitySet.FindAsync(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> rootSpecification,
            IAggregatSpecification<TEntity> treeSpecification = null)
        {
            if (treeSpecification == null)
            {
                return entitySet.Where(rootSpecification);
            }

            return BuildAggregate(treeSpecification.IncludeLeafs, treeSpecification.IncludeTrees)
                .Where(rootSpecification)
                .AsEnumerable();

        }


        public virtual IEnumerable<TEntity> Find(IRootSpecification<TEntity> rootSpecification,
            IAggregatSpecification<TEntity> treeSpecification = null)
        {
            return Find(rootSpecification.ToExpression(), treeSpecification);
        }

        protected IQueryable<TEntity> BuildAggregate(IEnumerable<Expression<Func<TEntity, object>>> includeChild, IEnumerable<string> includeTree)
        {
            var aggregateDbSet = entitySet.AsQueryable();
            if(includeChild != null) ApplyIncludeLeaf(includeChild, ref aggregateDbSet);
            if(includeTree != null) ApplyIncludeTree(includeTree, ref aggregateDbSet);

            return aggregateDbSet;
        }

        protected IQueryable<TEntity> ApplyIncludeTree(IEnumerable<string> includeTree, ref IQueryable<TEntity> currentDbSet)
        {
            return includeTree.Aggregate(currentDbSet, (current, include) => current.Include(include));
        }

        protected IQueryable<TEntity> ApplyIncludeLeaf(IEnumerable<Expression<Func<TEntity, object>>> includeChild, ref IQueryable<TEntity> currentDbSet)
        {
            return includeChild.Aggregate(currentDbSet, (current, include) => current.Include(include));
        }

    }
}
