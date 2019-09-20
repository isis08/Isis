using System.Linq;
using Isis.Architecture.Core.Domain.Entity;
using Isis.Architecture.Core.Domain.UnitTest.Entity.Seed;
using Newtonsoft.Json;
using Xunit;

namespace Isis.Architecture.Core.Domain.UnitTest.Entity
{
    public class EntityStateShould
    {
        [Fact]
        public void Entity_NewInstance_StateShould()
        {
            var entity = new ChildEntity();

            Assert.Equal(State.Unchanged, entity.State);
        }

        [Fact]
        public void Entity_NewInstanceFromSerialization_StateShould()
        {
            var entityJson = "{\"SimpleField\":2,\"FieldCollection\":[],\"Id\":0}";
            var entityFromSerialization = JsonConvert.DeserializeObject<ChildEntity>(entityJson);

            Assert.Equal(2, entityFromSerialization.SimpleField);
            Assert.Equal(State.Unchanged, entityFromSerialization.State);
        }

        [Fact]
        public void Entity_NewInstanceFromSerialization_ThenUpdate_StateShould()
        {
            var entityJson = "{\"SimpleField\":2,\"FieldCollection\":[],\"Id\":0}";
            var entityFromSerialization = JsonConvert.DeserializeObject<ChildEntity>(entityJson);

            Assert.Equal(2, entityFromSerialization.SimpleField);
            Assert.Equal(State.Unchanged, entityFromSerialization.State);

            // Act
            entityFromSerialization.SetSimpleField(4);

            Assert.Equal(4, entityFromSerialization.SimpleField);
            Assert.Equal(State.Modified, entityFromSerialization.State);
        }
        
        [Fact]
        public void Entity_Create_StateShould()
        {
            // Arrange
            var entity = new ChildEntity();
            Assert.Equal(State.Unchanged, entity.State);

            // Act
            entity.Create();

            // Assert
            Assert.Equal(State.Added, entity.State);
        }

        [Fact]
        public void Entity_Create_ThenUpdate_StateShould()
        {
            // Arrange
            var entity = new ChildEntity();
            Assert.Equal(State.Unchanged, entity.State);

            // Act
            entity.Create();
            Assert.Equal(State.Added, entity.State);
            entity.Id = 2;

            // Assert
            Assert.Equal(State.Added, entity.State);
        }
        
        [Fact]
        public void Entity_Delete_StateShould()
        {
            // Arrange
            var entity = new ChildEntity();
            Assert.Equal(State.Unchanged, entity.State);

            // Act
            entity.Delete();

            // Assert
            Assert.Equal(State.Deleted, entity.State);
        }

        [Fact]
        public void Entity_UpdateProperty_StateShould()
        {
            // Arrange
            var entity = new ChildEntity();
            Assert.Equal(State.Unchanged, entity.State);
            Assert.Equal(default(int), entity.SimpleField);

            // Act
            entity.SetSimpleField(1);

            // Assert
            Assert.Equal(1, entity.SimpleField);
            Assert.Equal(State.Modified, entity.State);
        }

        [Fact]
        public void Entity_UpdateCollection_StateShould()
        {
            // Arrange
            var entity = new ChildEntity();
            Assert.Empty(entity.FieldCollection);
            Assert.Equal(State.Unchanged, entity.State);

            // Act
            entity.FieldCollection.Add(10);

            // Assert
            Assert.Single(entity.FieldCollection);
            Assert.Equal(State.Modified, entity.State);
        }

    }
}
