using System;
using System.Linq.Expressions;
using Isis.Architecture.Core.Domain.Specification;

namespace Isis.Architecture.Pattern.Specification
{
    internal sealed class OrSpecification<T> : RootSpecification<T> where T : class
    {
        private readonly RootSpecification<T> _left;
        private readonly RootSpecification<T> _right;

        public OrSpecification(IRootSpecification<T> left, IRootSpecification<T> right)
        {
            _right = right as RootSpecification<T> ?? throw new InvalidCastException(nameof(right));
            _left = left as RootSpecification<T> ?? throw new InvalidCastException(nameof(left)); ;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.OrElse(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }
}
