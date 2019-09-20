using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Isis.Architecture.Core.Domain.Specification
{
    public interface IAggregatSpecification<TRoot> where TRoot : class
    {
        IEnumerable<Expression<Func<TRoot, object>>> IncludeLeafs { get; }

        IEnumerable<string> IncludeTrees { get; }

    }
}
