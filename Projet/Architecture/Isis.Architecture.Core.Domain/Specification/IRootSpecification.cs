using System;
using System.Linq.Expressions;

namespace Isis.Architecture.Core.Domain.Specification
{
    public interface IRootSpecification<T> where T : class
    {
        Expression<Func<T, bool>> ToExpression();

        bool IsSatisfiedBy(T entity);

        IRootSpecification<T> And(IRootSpecification<T> specification);

        IRootSpecification<T> Or(IRootSpecification<T> specification);

        IRootSpecification<T> Not();
    }
}
