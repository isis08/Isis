namespace Isis.Architecture.Core.Domain.Repository
{
    public interface IRepositoryDisconnected<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        void Detach(TEntity entity);

        void Attach(TEntity entity);

        void CreateOrUpdate(TEntity entity);
    }
}
