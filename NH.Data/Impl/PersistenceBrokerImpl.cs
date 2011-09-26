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
            return Session.Save(model);
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
    }
}