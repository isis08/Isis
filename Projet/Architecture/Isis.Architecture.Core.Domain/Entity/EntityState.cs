using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Isis.Architecture.Core.Domain.Entity
{
    public abstract class EntityState : EntityBase, IEntityState, INotifyPropertyChanged
    {
        protected EntityState()
        {
            PropertyChanged += SetPropertyChanged;
        }

        public State State { get; protected set; }

        private long _id;

        public override long Id
        {
            get => _id;
            set => _id = value;
        }

        public virtual void Create()
        {
            State = State.Added;
        }

        public virtual void Delete()
        {
            State = State.Deleted;
        }

        protected virtual void SetProperty<T>(ref T storage, T value)
        {
            if (Equals(storage, value)) return;
            storage = value;
            OnPropertyChanged();
        }

        protected virtual void SetCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            UpdateStatePropertyChanged();
        }

        protected virtual void SetPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            UpdateStatePropertyChanged();
        }

        private void UpdateStatePropertyChanged()
        {
            if (State == State.Unchanged) State = State.Modified;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
