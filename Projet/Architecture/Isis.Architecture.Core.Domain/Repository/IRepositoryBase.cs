using System;
using Isis.Architecture.Core.Domain.Specification;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Isis.Architecture.Core.Domain.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All(IAggregatSpecification<TEntity> querySpecification = null);

        TEntity Get(long id);

        IEnumerable<TEntity> Find(IRootSpecification<TEntity> rootSpecification,
            IAggregatSpecification<TEntity> specification);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> rootSpecification,
            IAggregatSpecification<TEntity> specification);
    }
}
