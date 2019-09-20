using System;
using System.Data.SQLite;
using System.Linq;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Context;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest
{
    public class UowShould : TestBase
    {

        [Fact]
        public void AddThenUpdateWithError()
        {
            //-- In-memory database only exists while the connection is open
            var connection = new SQLiteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TestContextUow>()
                .UseSqlite(connection)
                .Options;

            var newName = Guid.NewGuid().ToString();

            try
            {
                //-- Create the schema in the database
                using (var ctxt = new TestContextUow(options))
                {
                    ctxt.Database.EnsureCreated();
                }

                //-- Run the test against one instance of the context
                using (var ctxt = new TestContextUow(options))
                {
                    //-- Start explicit transaction
                    ctxt.BeginTransaction();

                    var repository = ctxt.MyDisconnectedEntities;

                    //-- Add entity
                    var initialName = Guid.NewGuid().ToString();
                    var entity = new MyEntity()
                    {
                        Id = 1,
                        Name = initialName,
                        MyNestedEntity = new MyNestedEntity { Id = 1, Name = Guid.NewGuid().ToString() }
                    };

                    repository.Add(entity);
                    ctxt.Save();

                    //-- Detach the item in order get a different instance
                    ctxt.MyDisconnectedEntities.Detach(entity);

                    //-- Fetch the item and update title
                    var newItem = repository.All().FirstOrDefault(i => i.Name == initialName);
                    Assert.NotNull(newItem);
                    Assert.Same(entity, newItem);

                    newItem.Name = newName;

                    //-- Update the entity
                    repository.Update(newItem);

                    //-- Save context
                    ctxt.Save();

                    //-- Generate ERROR 
                    throw new Exception("My integration test exception");

                    //-- Comit transaction
#pragma warning disable 162
                    ctxt.CommitTransaction();
                }

            }
#pragma warning disable 168
            catch (Exception e)
            {
                #region Assert

                //-- Use a separate instance of the context to verify correct data was saved to database
                using (var ctxt = new TestContextUow(options))
                {
                    var repository = ctxt.MyDisconnectedEntities;
                    Assert.Null(repository.All().FirstOrDefault(i => i.Name == newName));
                }

                #endregion
            }
            finally
            {
                connection.Close();
            }

        }


        [Fact]
        public void AddThenUpdateWithoutError()
        {
            //-- In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TestContextUow>()
                .UseSqlite(connection, x => x.SuppressForeignKeyEnforcement(false))
                .Options;


            try
            {
                //-- Create the schema in the database
                using (var ctxt = new TestContextUow(options))
                {
                    ctxt.Database.EnsureCreated();
                }

                //-- Run the test against one instance of the context
                using (var ctxt = new TestContextUow(options))
                {
                    #region Arrange

                    //-- Start explicit transaction
                    ctxt.BeginTransaction();

                    var repository = ctxt.MyDisconnectedEntities;

                    //-- Add entity
                    var initialName = Guid.NewGuid().ToString();
                    var entity = new MyEntity()
                    {
                        Id = 1,
                        Name = initialName,
                        MyNestedEntity = new MyNestedEntity { Id = 1, Name = Guid.NewGuid().ToString() }
                    };

                    repository.Add(entity);
                    ctxt.Save();

                    //-- Detach the item in order get a different instance
                    ctxt.MyDisconnectedEntities.Detach(entity);

                    //-- Fetch the item and update title
                    var newEntity = repository.All().FirstOrDefault(i => i.Name == initialName);
                    Assert.NotNull(newEntity);
                    Assert.Same(entity, newEntity);
                    var newName = Guid.NewGuid().ToString();
                    newEntity.Name = newName;

                    #endregion

                    #region Act

                    //-- Update the entity
                    repository.Update(newEntity);

                    //-- Save context
                    ctxt.Save();

                    var entityUpdated = repository.All().FirstOrDefault(i => i.Name == newName);
                    
                    //-- Commit transaction
                    ctxt.CommitTransaction();

                    #endregion

                    #region Assert

                    Assert.NotNull(entityUpdated);
                    Assert.Equal(newName, entityUpdated.Name);
                    Assert.Equal(newEntity.Id, entityUpdated.Id);

                    #endregion
                }
            }
            finally
            {
                connection.Close();
            }

        }


    }
}
