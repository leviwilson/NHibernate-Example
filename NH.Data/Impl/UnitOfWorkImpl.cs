using System.Data;
using NHibernate;

namespace NH.Data.Impl
{
    public class UnitOfWorkImpl : UnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _currentTransaction;

        public UnitOfWorkImpl(ISession session)
        {
            _session = session;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _currentTransaction = _session.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            _currentTransaction.Commit();
        }

        public void Rollback()
        {
            _currentTransaction.Rollback();
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
            if( null != _currentTransaction)
                _currentTransaction.Dispose();

            _session.Dispose();
        }
    }
}