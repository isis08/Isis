using System;
using System.Linq.Expressions;
using Isis.Architecture.Core.Domain.Specification;

namespace Isis.Architecture.Pattern.Specification
{
    public abstract class RootSpecification<T> : IRootSpecification<T> where T : class
    {
        public static readonly IRootSpecification<T> All = new IdentitySpecification<T>();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public IRootSpecification<T> And(IRootSpecification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public IRootSpecification<T> Or(IRootSpecification<T> specification)
        {
            if (this == All || specification == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public IRootSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}
