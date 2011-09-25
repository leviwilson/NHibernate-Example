using System.Linq;

namespace NH.Data
{
    public interface PersistenceBroker<TConfig> where TConfig : SessionConfiguration
    {
        object Create(object model);
        T Get<T>(object id);
        IQueryable<T> Query<T>();
        void Delete(object model);
    }
}