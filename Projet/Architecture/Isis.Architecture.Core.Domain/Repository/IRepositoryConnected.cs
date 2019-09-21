using System.Collections.Generic;
using System.Threading.Tasks;

namespace Isis.Architecture.Core.Domain.Repository
{
    public interface IRepositoryConnected<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        Task AddAsync(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);
    }
}
