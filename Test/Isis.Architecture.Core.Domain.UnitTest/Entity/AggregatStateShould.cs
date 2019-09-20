using System.Linq;
using Isis.Architecture.Core.Domain.Entity;
using Isis.Architecture.Core.Domain.UnitTest.Entity.Seed;
using Xunit;

namespace Isis.Architecture.Core.Domain.UnitTest.Entity
{
    public class AggregatStateShould
    {
        [Fact]
        public void RootEntity_NewInstance_StateShould()
        {
            var root = new RootEntity();
            Assert.Equal(State.Unchanged, root.State);
        }

        [Fact]
        public void RootEntity_AddNewChild_StateShould()
        {
            // Arrange
            var root = new RootEntity();
            Assert.Equal(State.Unchanged, root.State);
            Assert.Null(root.ChildEntity);

            var newChild = new ChildEntity();
            Assert.Equal(State.Unchanged, newChild.State);

            newChild.Create();
            Assert.Equal(State.Added, newChild.State);

            // Act
            root.SetChildEntity(newChild);

            // Assert
            Assert.Equal(State.Modified, root.State);
            Assert.Equal(State.Added, root.ChildEntity.State);
        }

        [Fact]
        public void RootEntity_RemoveExistingChild_StateShould()
        {
            // Arrange
            var root = new RootEntity(new ChildEntity());
            Assert.Equal(State.Unchanged, root.State);
            Assert.Equal(State.Unchanged, root.ChildEntity.State);

            // Act
            var childEntity = root.ChildEntity;
            root.RemoveChildEntity();

            // Assert
            Assert.Equal(State.Modified, root.State);
            Assert.Equal(State.Unchanged, childEntity.State);
            Assert.Null(root.ChildEntity);
        }

        [Fact]
        public void RootEntity_DeleteExistingChild_StateShould()
        {
            // Arrange
            var root = new RootEntity(new ChildEntity());
            Assert.Equal(State.Unchanged, root.State);
            Assert.Equal(State.Unchanged, root.ChildEntity.State);

            // Act
            root.DeleteChildEntity();

            // Assert
            Assert.Equal(State.Modified, root.State);
            Assert.Null(root.ChildEntity);
        }

        [Fact]
        public void RootEntity_UpdateChildProperty_StateShould()
        {
            // Arrange
            var root = new RootEntity(new ChildEntity());
            Assert.Equal(State.Unchanged, root.State);
            Assert.Equal(State.Unchanged, root.ChildEntity.State);

            // Act
            root.ChildEntity.SetSimpleField(1);

            // Assert
            Assert.Equal(State.Unchanged, root.State);
            Assert.Equal(State.Modified, root.ChildEntity.State);
        }

        [Fact]
        public void RootEntity_AddExistingChildCollection_StateShould()
        {
            // Arrange
            var root = new RootEntity();
            Assert.Equal(State.Unchanged, root.State);

            var child = new ChildEntity();
            Assert.Equal(State.Unchanged, child.State);

            // Act
            root.AddChildCollection(child);
            Assert.Single(root.ChildCollection);
            Assert.Equal(State.Unchanged, root.ChildCollection.FirstOrDefault().State);

            root.ChildCollection.Add(new ChildEntity());
            Assert.Equal(2, root.ChildCollection.Count);

            // Assert
            Assert.Equal(State.Modified, root.State);
            Assert.Equal(State.Unchanged, root.ChildCollection[0].State);
            Assert.Equal(State.Unchanged, root.ChildCollection[1].State);
        }

    }
}
