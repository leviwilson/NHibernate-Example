using System.Linq;
using NH.Data.Customers.Config;
using NH.Data.Customers.Models;

namespace NH.Data.Customers.Repositories.Impl
{
    public class RestaurantRepositoryImpl : RestaurantRepository
    {
        private readonly PersistenceBroker<RestaurantConfiguration> _persistenceBroker;

        public RestaurantRepositoryImpl(PersistenceBroker<RestaurantConfiguration> persistenceBroker)
        {
            _persistenceBroker = persistenceBroker;
        }

        public Restaurant FindById(int id)
        {
            return _persistenceBroker.Query<Restaurant>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}