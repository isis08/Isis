using System;

namespace Isis.Architecture.Core.Domain.Entity
{
    public abstract class EntityBase : IEntityBase, ICloneable
    {
        public virtual long Id { get; set; }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

    }
}
