using System.Linq;

namespace NH.Data
{
// ReSharper disable UnusedTypeParameter
    public interface PersistenceBroker<TConfig> where TConfig : SessionConfiguration
// ReSharper restore UnusedTypeParameter
    {
        object Create(object model);
        T Get<T>(object id);
        IQueryable<T> Query<T>();
        void Delete(object model);
    }
}