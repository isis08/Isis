using System;
using System.Linq.Expressions;
using Isis.Architecture.Core.Domain.Specification;

namespace Isis.Architecture.Pattern.Specification
{
    internal sealed class AndSpecification<T> : RootSpecification<T> where T : class
    {
        private readonly RootSpecification<T> _left;
        private readonly RootSpecification<T> _right;

        public AndSpecification(IRootSpecification<T> left, IRootSpecification<T> right)
        {
            _right = right as RootSpecification<T>;
            _left = left as RootSpecification<T>;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.AndAlso(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }
}
