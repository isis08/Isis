using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Isis.Architecture.Core.Domain.Specification;

namespace Isis.Architecture.Pattern.Specification
{
    public class AggregatSpecification<TRoot> : IAggregatSpecification<TRoot> where TRoot : class
    {
        public AggregatSpecification(IEnumerable<string> includeTrees = null, params Expression<Func<TRoot, object>>[] includeLeafs)
        {
            IncludeLeafs = includeLeafs;
            IncludeTrees = includeTrees ?? new List<string>();
        }

        public IEnumerable<Expression<Func<TRoot, object>>> IncludeLeafs { get; }

        public IEnumerable<string> IncludeTrees { get; }
    }
}
