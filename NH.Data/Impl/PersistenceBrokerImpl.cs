using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace NH.Data.Impl
{
    public class PersistenceBrokerImpl<TConfig> : PersistenceBroker<TConfig> where TConfig : SessionConfiguration
    {
        private static ISession Session
        {
            get { return SessionDataContext.Get<TConfig>(); }
        }

        public object Create(object model)
        {
            Session.Save(model);
            return model;
        }

        public T Get<T>(object id)
        {
            return Session.Get<T>(id);
        }

        public IQueryable<T> Query<T>()
        {
            return Session.Query<T>();
        }

        public IQuery GetNamedQuery(string queryName)
        {
            return Session.GetNamedQuery(queryName);
        }

        public void Delete(object model)
        {
            Session.Delete(model);
        }

        public ISQLQuery CreateSqlQuery(string queryString)
        {
            return Session.CreateSQLQuery(queryString);
        }

        public IQuery CreateHqlQuery(string queryString)
        {
            return Session.CreateQuery(queryString);
        }

        public IDbCommand CreateDbCommand()
        {
            var dbCommand = Session.Connection.CreateCommand();
            dbCommand.CommandTimeout = 600;

            if( null != Session.Transaction )
                Session.Transaction.Enlist(dbCommand);

            return dbCommand;
        }
    }
}