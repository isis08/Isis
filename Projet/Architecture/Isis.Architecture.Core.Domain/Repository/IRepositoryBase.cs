using System;
using Isis.Architecture.Core.Domain.Specification;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Isis.Architecture.Core.Domain.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<List<TEntity>> AllAsync(IAggregatSpecification<TEntity> querySpecification = null);

        Task<TEntity> GetAsync(long id);

        Task<TEntity> FindSingleAsync(IRootSpecification<TEntity> rootSpecification,
            IAggregatSpecification<TEntity> specification = null);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> rootSpecification,
            IAggregatSpecification<TEntity> specification = null);

        Task<List<TEntity>> FindAsync(IRootSpecification<TEntity> rootSpecification,
            IAggregatSpecification<TEntity> specification = null);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> rootSpecification,
            IAggregatSpecification<TEntity> specification = null);
    }
}
