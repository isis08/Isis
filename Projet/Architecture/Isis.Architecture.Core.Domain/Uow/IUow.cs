using System.Data;

namespace Isis.Architecture.Core.Domain.Uow
{
    public interface IUow
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void SetIsolationLevel(IsolationLevel isolationLevel);
    }
}
