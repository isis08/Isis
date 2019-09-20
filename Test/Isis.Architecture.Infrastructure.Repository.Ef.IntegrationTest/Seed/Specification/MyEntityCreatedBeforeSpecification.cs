using System;
using System.Linq.Expressions;
using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Isis.Architecture.Pattern.Specification;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Specification
{
    internal class MyEntityCreatedBeforeSpecification : RootSpecification<MyEntity>
    {
        private readonly DateTime _dateDeadLine;

        public MyEntityCreatedBeforeSpecification(DateTime date)
        {
            _dateDeadLine = date;
        }

        public override Expression<Func<MyEntity, bool>> ToExpression()
        {
            return myEntity => myEntity.AddedDate <= _dateDeadLine;
        }
    }
}
