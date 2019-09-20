using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Configuration;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Repository;
using Microsoft.EntityFrameworkCore;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Context
{
    public class TestContextUow : Uow
    {
        public virtual MyEntityRepositoryConnected MyConnectedEntities { get; }

        public virtual MyEntityRepositoryDisconnected MyDisconnectedEntities { get; }

        public TestContextUow(DbContextOptions options) : base(options)
        {
            MyConnectedEntities = new MyEntityRepositoryConnected(this);
            MyDisconnectedEntities = new MyEntityRepositoryDisconnected(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies(false);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-- Create sense project model
            base.OnModelCreating(modelBuilder);

            //-- Add tests model
            new MyEntityMap(modelBuilder.Entity<MyEntity>());
            new MyNestedEntityMap(modelBuilder.Entity<MyNestedEntity>());
        }
    }
}
