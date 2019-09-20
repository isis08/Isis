using System.Collections.ObjectModel;
using Isis.Architecture.Core.Domain.Entity;

namespace Isis.Architecture.Core.Domain.UnitTest.Entity.Seed
{
    public class ChildEntity : EntityState
    {
        public ChildEntity()
        {
            FieldCollection = new ObservableCollection<int>();
            FieldCollection.CollectionChanged += SetCollectionChanged;
        }

        private int _simpleField;

        public int SimpleField
        {
            get => _simpleField;
            set => _simpleField = value;
        }

        public ObservableCollection<int> FieldCollection { get; }

        public void SetSimpleField(int simpleFieldValue)
        {
            SetProperty(ref _simpleField, simpleFieldValue);
        }

    }
}
