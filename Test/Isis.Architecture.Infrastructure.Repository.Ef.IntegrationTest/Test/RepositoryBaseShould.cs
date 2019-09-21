using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Isis.Architecture.Pattern.Specification;
using Xunit;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest
{
    public class RepositoryBaseShould : TestBase
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnEntities()
        {
            #region Arrange

            var repositoryBase = Context.BaseEntities;
            var repositoryConnected = Context.MyConnectedEntities;

            var entity1 = new MyEntity()
            {
                Name = new Guid().ToString()
            };

            var entity2 = new MyEntity()
            {
                Name = new Guid().ToString()
            };

            #endregion

            #region Act

            //-- Add 2 entities
            await repositoryConnected.AddAsync(entity1);
            await repositoryConnected.AddAsync(entity2);

            await Context.SaveAsync();
            var entitiesAdded = await repositoryBase.AllAsync();

            #endregion

            #region Assert

            Assert.Equal(2, entitiesAdded.Count);
            Assert.Equal(entity1.Name, entitiesAdded.FirstOrDefault(x => x.Name == entity1.Name)?.Name);
            Assert.Equal(entity2.Name, entitiesAdded.FirstOrDefault(x => x.Name == entity2.Name)?.Name);
            Assert.True(entitiesAdded.FirstOrDefault(x => x.Name == entity1.Name)?.Id >= 0);
            Assert.True(entitiesAdded.FirstOrDefault(x => x.Name == entity2.Name)?.Id >= 0);

            #endregion
        }

        [Fact]
        public async Task FindSingleAsyncShouldReturnEntity()
        {
            #region Arrange

            var repositoryConnected = Context.MyConnectedEntities;
            var repositoryBase = Context.BaseEntities;

            var entity1 = new MyEntity()
            {
                Name = new Guid().ToString()
            };

            var entity2 = new MyEntity()
            {
                Name = new Guid().ToString()
            };

            #endregion

            #region Act

            //-- Add 2 entities
            await repositoryConnected.AddAsync(entity1);
            await repositoryConnected.AddAsync(entity2);
            await Context.SaveAsync();

            var entity1Added = await repositoryBase.FindSingleAsync(x=>x.Name == entity1.Name);
            var entity2Added = await repositoryBase.FindSingleAsync(x=>x.Name == entity2.Name);

            #endregion

            #region Assert

            Assert.NotNull(entity1Added);
            Assert.NotNull(entity2Added);

            Assert.Equal(entity1.Name, entity1Added.Name);
            Assert.Equal(entity2.Name, entity2Added.Name);

            Assert.True(entity1Added.Id >= 0);
            Assert.True(entity2Added.Id >= 0);

            #endregion
        }

        [Fact]
        public async Task FindSingleAsyncShouldReturnTree()
        {
            #region Arrange

            var repositoryConnected = Context.MyConnectedEntities;
            var repositoryBase = Context.BaseEntities;

            var nestedEntities = new List<MyNestedEntity>
            {
                new MyNestedEntity()
                {
                    Id = 1,
                    Name = Guid.NewGuid().ToString()
                }
                ,new MyNestedEntity()
                {
                    Id = 2,
                    Name = Guid.NewGuid().ToString()
                }
            };

            var entities = new List<MyEntity>
            {
                new MyEntity()
                {
                    Id = 1,
                    Name = Guid.NewGuid().ToString(),
                    MyNestedEntity = nestedEntities[0]
                }
                ,new MyEntity()
                {
                    Id = 2,
                    Name = Guid.NewGuid().ToString(),
                    MyNestedEntity = nestedEntities[1]
                }
            };

            //-- Add and Get many
            await repositoryConnected.AddAsync(entities);
            await Context.SaveAsync();

            #endregion

            #region Act

            var aggregatSpecification = new AggregatSpecification<MyEntity>(includeLeafs: i => i.MyNestedEntity);
            var entitiesAdded = await repositoryBase.FindSingleAsync(x=>x.Id == 1, aggregatSpecification);

            #endregion

            #region Assert

            Assert.NotNull(entitiesAdded);
            Assert.NotNull(entitiesAdded.MyNestedEntity);

            #endregion
        }

    }
}
