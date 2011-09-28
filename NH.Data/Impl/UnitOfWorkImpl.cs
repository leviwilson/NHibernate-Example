using System;
using System.Data;
using NHibernate;

namespace NH.Data.Impl
{
    public class UnitOfWorkImpl : UnitOfWork
    {
        private readonly ISession _session;
        private bool _isDisposed;

        public UnitOfWorkImpl(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _session.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            if (_session.Transaction.IsActive)
                _session.Transaction.Commit();
        }

        public void Rollback()
        {
            if ( _session.Transaction.IsActive)
                _session.Transaction.Rollback();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public void Clear()
        {
            _session.Clear();
        }

        public void Evict(object model)
        {
            _session.Evict(model);
        }

        public void Refresh(object model)
        {
            _session.Refresh(model);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool doDispose)
        {
            if (_isDisposed || !doDispose) return;

            Rollback();

            _session.Dispose();
            GC.SuppressFinalize(this);
            _isDisposed = true;
        }
    }
}