using System;
using System.Linq;
using System.Linq.Expressions;

namespace Isis.Architecture.Pattern.Specification
{
    internal sealed class NotSpecification<T> : RootSpecification<T> where T : class
    {
        private readonly RootSpecification<T> _specification;

        public NotSpecification(RootSpecification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
