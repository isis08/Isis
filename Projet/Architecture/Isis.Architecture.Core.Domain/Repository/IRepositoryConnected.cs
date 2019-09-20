using System.Collections.Generic;

namespace Isis.Architecture.Core.Domain.Repository
{
    public interface IRepositoryConnected<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entity);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entity);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entity);
    }
}
