using Isis.Architecture.Core.Domain.Repository;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Interface
{
    public interface IMyEntityRepositoryDisconnected : IRepositoryDisconnected<MyEntity>
    {
    }
}
