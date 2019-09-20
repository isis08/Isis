using System.Collections.ObjectModel;
using Isis.Architecture.Core.Domain.Entity;

namespace Isis.Architecture.Core.Domain.UnitTest.Entity.Seed
{
    public class RootEntity : EntityState
    {
        public RootEntity()
        {
            ChildCollection = new ObservableCollection<ChildEntity>();
            ChildCollection.CollectionChanged += SetCollectionChanged;
        }

        public RootEntity(ChildEntity child) : this()
        {
            _childEntity = child;
        }

        private ChildEntity _childEntity;

        public ChildEntity ChildEntity
        {
            get => _childEntity;
            set => _childEntity = value;
        }

        public ObservableCollection<ChildEntity> ChildCollection { get; }

        public void AddChildCollection(ChildEntity child)
        {
            ChildCollection.Add(child);
        }

        public void SetChildEntity(ChildEntity child)
        {
            SetProperty(ref _childEntity, child);
        }

        public void RemoveChildEntity()
        {
            SetProperty(ref _childEntity, null);
        }

        /// <summary>
        /// All operation must be done in the root entity of the tree !!
        /// </summary>
        public void DeleteChildEntity()
        {
            ChildEntity.Delete();
            RemoveChildEntity();
        }

    }
}
