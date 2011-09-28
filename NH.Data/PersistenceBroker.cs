using System.Data;
using System.Linq;
using NHibernate;

namespace NH.Data
{
// ReSharper disable UnusedTypeParameter
    public interface PersistenceBroker<TConfig> where TConfig : SessionConfiguration
// ReSharper restore UnusedTypeParameter
    {
        object Create(object model);
        T Get<T>(object id);
        IQueryable<T> Query<T>();
        IQuery GetNamedQuery(string queryName);
        void Delete(object model);
        ISQLQuery CreateSqlQuery(string queryString);
        IQuery CreateHqlQuery(string queryString);
        IDbCommand CreateDbCommand();
    }
}