using System;

namespace Isis.Architecture.Core.Domain.Entity
{
    public abstract class EntityBase : IEntityBase, ICloneable
    {
        private long _id;

        public virtual long Id
        {
            get => _id;
            set
            {
                if(_id.Equals(value)) return;
                _id = value;
            }
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

    }
}
