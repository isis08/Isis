using System;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest
{
    public abstract class TestBase
    {
        /// <summary>
        /// Use FrameworkInMemory
        /// </summary>
        public TestContext Context;

        protected TestBase()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddEntityFrameworkProxies()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies(false)
                .UseInternalServiceProvider(serviceProvider);

            this.Context = new TestContext(builder.Options);
        }
        
    }
}
