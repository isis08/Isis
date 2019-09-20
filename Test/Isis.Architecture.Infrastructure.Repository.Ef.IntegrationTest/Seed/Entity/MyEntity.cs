using System;
using Isis.Architecture.Core.Domain.Entity;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Interface;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity
{
    public class MyEntity : EntityState, IMyEntity
    {
        public MyEntity() {}

        public override long Id { get; set; }

        public long MyNestedEntityId { get; set; }

        public virtual MyNestedEntity MyNestedEntity { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private DateTime? _addedDate;

        private DateTime? _modifiedLastDate;

        public virtual DateTime? AddedDate
        {
            get => _addedDate ?? (_addedDate = DateTime.Now);
            set => _addedDate = value;
        }

        public virtual DateTime? ModifiedLastDate
        {
            get => _modifiedLastDate;
            set => _modifiedLastDate = value;
        }

    }
}
