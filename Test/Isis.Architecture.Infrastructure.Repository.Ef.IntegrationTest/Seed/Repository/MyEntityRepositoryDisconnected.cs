using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Interface;
using Microsoft.EntityFrameworkCore;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Repository
{
    public class MyEntityRepositoryDisconnected : RepositoryDisconnected<MyEntity>, IMyEntityRepositoryDisconnected
    {
        public MyEntityRepositoryDisconnected(DbContext context) : base(context)
        {
        }
    }
}
