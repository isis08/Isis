using System;
using System.Linq.Expressions;

namespace Isis.Architecture.Pattern.Specification
{
    internal sealed class IdentitySpecification<T> : RootSpecification<T> where T : class
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}
