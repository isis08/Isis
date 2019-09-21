using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Specification;
using Isis.Architecture.Pattern.Specification;
using Xunit;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest
{
    public class RepositoryDisconnectedShould : TestBase
    {

        [Fact]
        public async Task AddAndSetId()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var entity = new MyEntity()
            {
                Name = "Name"
            };

            #endregion

            #region Act

            repository.Add(entity);
            Context.Save();
            var entityAdded = await repository.FindSingleAsync(x => x.Name == "Name");

            #endregion

            #region Assert

            Assert.Equal(entity, entityAdded);
            Assert.True(entityAdded?.Id > 0);

            #endregion
        }



        [Fact]
        public async Task UpdateAfterAdded()
        {
            #region Arrange

            //-- Add entity
            var repository = Context.MyDisconnectedEntities;

            var initialName = Guid.NewGuid().ToString();

            var entity = new MyEntity()
            {
                Name = initialName
            };

            repository.Add(entity);
            Context.Save();

            //-- Detach the item in order get a different instance
            repository.Detach(entity);

            //-- Fetch the item and update title
            var newEntity = await repository.FindSingleAsync(i => i.Name == initialName);

            Assert.NotNull(newEntity);
            Assert.Same(entity, newEntity);

            #endregion

            #region Act

            //-- Update the entity
            var newTitle = Guid.NewGuid().ToString();
            newEntity.Name = newTitle;
            repository.Update(newEntity);
            Context.Save();

            var entityUpdated = await repository.FindSingleAsync(i => i.Name == newTitle);

            #endregion

            #region Assert

            Assert.NotNull(entityUpdated);
            Assert.Equal(newTitle, entityUpdated.Name);
            Assert.Equal(newEntity.Id, entityUpdated.Id);

            #endregion
        }

        [Fact]
        public async Task DeleteAfterAdded()
        {
            #region Arrange

            //-- Add entity
            var repository = Context.MyDisconnectedEntities;

            var initialName = Guid.NewGuid().ToString();

            var entity = new MyEntity()
            {
                Name = initialName
            };

            repository.Add(entity);
            Context.Save();
            var entityAdded = await repository.FindSingleAsync(i => i.Name == initialName);

            Assert.NotNull(entityAdded);

            #endregion

            #region Act

            //-- Delete the item
            repository.Delete(entity);
            Context.Save();

            #endregion

            #region Assert

            var resultEntities = await repository.AllAsync();
            Assert.DoesNotContain(resultEntities, i => i.Name == initialName);

            #endregion
        }

        [Fact]
        public async Task AddAndFindAsync()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var entityAfter = new MyEntity()
            {
                AddedDate = DateTime.Now.AddDays(1),
                Name = "Component after"
            };

            var entityBefore = new MyEntity()
            {
                AddedDate = DateTime.Now.AddDays(-1),
                Name = "Component before"
            };

            //-- Add 2 entities
            repository.Add(entityAfter);
            repository.Add(entityBefore);
            Context.Save();
            var nbAdded = await repository.AllAsync();

            #endregion

            #region Act

            RootSpecification<MyEntity> createdBefore = new MyEntityCreatedBeforeSpecification(DateTime.Now);
            var entities = await repository.FindAsync(createdBefore);

            #endregion

            #region Assert

            Assert.Equal(2, nbAdded.Count());
            Assert.Single(entities);
            Assert.Equal("Component before", entities.FirstOrDefault().Name);

            #endregion
        }

        [Fact]
        public async Task AddMany()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var entities = new List<MyEntity>
            {
                new MyEntity()
                {
                    Name = Guid.NewGuid().ToString()
                }
                ,new MyEntity()
                {
                    Name = Guid.NewGuid().ToString()
                }
            };

            #endregion

            #region Act

            //-- Act
            repository.Add(entities);
            Context.Save();
            var entitiesAdded = await repository.AllAsync();

            #endregion

            #region Assert

            //-- Assert
            Assert.Equal(entities, entitiesAdded);
            Assert.Equal(entities.Count, entitiesAdded.Count());

            #endregion
        }

        [Fact]
        public async Task UpdateMany()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var initialName1 = Guid.NewGuid().ToString();
            var initialName2 = Guid.NewGuid().ToString();

            var entities = new List<MyEntity>
            {
                new MyEntity()
                {
                    Name = initialName1
                }
                ,new MyEntity()
                {
                    Name = initialName2
                }
            };

            //-- Add and Get many
            repository.Add(entities);
            Context.Save();
            var entitiesAdded = await repository.AllAsync();

            var names = entitiesAdded.Select(e => e.Name);
            Assert.Contains(initialName1, names);
            Assert.Contains(initialName2, names);

            #endregion

            #region Act

            var updatedName1 = Guid.NewGuid().ToString();
            var updatedName2 = Guid.NewGuid().ToString();

            entitiesAdded.First(e => e.Name == initialName1).Name = updatedName1;
            entitiesAdded.First(e => e.Name == initialName2).Name = updatedName2;

            repository.Update(entitiesAdded);
            Context.Save();
            var entitiesUpdated = await repository.AllAsync();

            #endregion

            #region Assert

            var namesUpdated = entitiesUpdated.Select(e => e.Name);
            Assert.Contains(updatedName1, namesUpdated);
            Assert.Contains(updatedName2, namesUpdated);


            #endregion
        }

        [Fact]
        public async Task DeleteMany()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var entities = new List<MyEntity>
            {
                new MyEntity()
                {
                    Name = Guid.NewGuid().ToString()
                }
                ,new MyEntity()
                {
                    Name = Guid.NewGuid().ToString()
                }
            };

            //-- Add and Get many
            repository.Add(entities);
            Context.Save();
            var entitiesAdded = await repository.AllAsync();

            var nbEntities = entitiesAdded.Count();
            Assert.Equal(2, nbEntities);

            #endregion

            #region Act

            repository.Delete(entitiesAdded);
            Context.Save();

            #endregion

            #region Assert

            var nb = repository.AllAsync().Result.Count();
            Assert.Equal(0, nb);

            #endregion
        }

        [Fact]
        public async Task AddManyAndGetAll()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var entities = new List<MyEntity>
            {
                new MyEntity()
                {
                    Name = Guid.NewGuid().ToString()
                }
                ,new MyEntity()
                {
                    Name = Guid.NewGuid().ToString()
                }
            };

            //-- Add and Get many
            repository.Add(entities);
            Context.Save();

            #endregion

            #region Act

            var entitiesAdded = await repository.AllAsync();

            #endregion

            #region Assert

            var nbEntities = entitiesAdded.Count();
            Assert.Equal(2, nbEntities);

            #endregion
        }

        [Fact]
        public void AddManyTreeAndGetAllWithInclude()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

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
            repository.Add(entities);
            Context.Save();

            #endregion

            #region Act

            var aggregatSpecification = new AggregatSpecification<MyEntity>( includeLeafs:i => i.MyNestedEntity);
            var entitiesAdded = repository.AllAsync(aggregatSpecification).Result.OrderBy(o => o.Id);
            var nbEntities = entitiesAdded.Count();

            #endregion

            #region Assert

            Assert.Equal(2, nbEntities);
            Assert.NotNull(entitiesAdded.First().MyNestedEntity);
            Assert.NotNull(entitiesAdded.ElementAt(1).MyNestedEntity);
            Assert.Equal(1, entitiesAdded.First().MyNestedEntity.Id);
            Assert.Equal(2, entitiesAdded.ElementAt(1).MyNestedEntity.Id);

            #endregion
        }

        [Fact]
        public void AddManyTreeAndGetAllWithStringInclude()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

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
            repository.Add(entities);
            Context.Save();

            #endregion

            #region Act

            var aggregatSpecification = new AggregatSpecification<MyEntity>(includeTrees: new[] { "MyNestedEntity" });
            var entitiesAdded = repository.AllAsync(aggregatSpecification).Result.OrderBy(o => o.Id);
            var nbEntities = entitiesAdded.Count();

            #endregion

            #region Assert

            Assert.Equal(2, nbEntities);
            Assert.NotNull(entitiesAdded.First().MyNestedEntity);
            Assert.NotNull(entitiesAdded.ElementAt(1).MyNestedEntity);
            Assert.Equal(1, entitiesAdded.First().MyNestedEntity.Id);
            Assert.Equal(2, entitiesAdded.ElementAt(1).MyNestedEntity.Id);

            #endregion
        }



        [Fact]
        public async Task AddTreeAndFindWithInclude()
        {
            #region Arrange

            var repository = Context.MyDisconnectedEntities;

            var nestedEntityBefore = new MyNestedEntity()
            {
                Id = 1,
                Name = Guid.NewGuid().ToString()
            };

            var entityBefore = new MyEntity()
            {
                Id = 1,
                AddedDate = DateTime.Now.AddDays(-1),
                Name = "Component before",
                MyNestedEntity = nestedEntityBefore
            };

            repository.Add(entityBefore);
            Context.Save();

            #endregion

            #region Act

            RootSpecification<MyEntity> createdBefore = new MyEntityCreatedBeforeSpecification(DateTime.Now);
            var entities = await repository.FindAsync(createdBefore);

            #endregion

            #region Assert

            Assert.NotNull(entities);
            Assert.Equal(1, entities.First().Id);
            Assert.NotNull(entities.First().MyNestedEntity);
            Assert.Equal(1, entities.First().MyNestedEntity.Id);
            Assert.Equal(1, entities.First().Id);

            #endregion
        }
    }
}
