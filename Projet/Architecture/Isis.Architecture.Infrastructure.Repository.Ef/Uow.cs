using System.Data;
using Isis.Architecture.Core.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Isis.Architecture.Infrastructure.Repository.Ef
{
    public class Uow : ContextBase, IUow
    {
        private IDbContextTransaction _transaction;
        private IsolationLevel? _isolationLevel;

        /// <inheritdoc />
        /// <summary>
        /// Allow DbConnection to be externally provided
        /// </summary>
        /// <param name="options"></param>
        public Uow(DbContextOptions options) : base(options)
        {
        }

        private void StartNewTransactionIfNeeded()
        {
            if (_transaction != null) return;

            _transaction = _isolationLevel.HasValue
                ? Database.BeginTransaction(_isolationLevel.GetValueOrDefault())
                : Database.BeginTransaction();
        }

        public void BeginTransaction()
        {
            StartNewTransactionIfNeeded();
        }

        public void CommitTransaction()
        {
            //-- Do not open transaction here, because if during the request
            //-- nothing was changed (only select queries were run), we don't
            //-- want to open and commit an empty transaction - calling SaveChanges()
            //-- on _transactionProvider will not send any sql to database in such case
            SaveChangesAsync();

            if (_transaction == null) return;

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();

            _transaction.Dispose();
            _transaction = null;
        }

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

        #region IDisposable implementation

        public override void Dispose()
        {
            base.Dispose();
            _transaction?.Dispose();
            _transaction = null;
        }

        #endregion
    }
}
