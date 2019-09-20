using System.Threading.Tasks;
using Isis.Architecture.Core.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Isis.Architecture.Infrastructure.Repository.Ef
{
    public abstract class ContextBase : DbContext, IContext
    {
        /// <inheritdoc />
        /// <param name="options"></param>
        protected ContextBase(DbContextOptions options) : base(options)
        {
        }

        public virtual int Save()
        {
            return SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }
    }
}
