using System;
using System.Threading.Tasks;

namespace Isis.Architecture.Core.Domain.Context
{
    public interface IContext : IDisposable
    {
        int Save();

        Task<int> SaveAsync();

    }
}
