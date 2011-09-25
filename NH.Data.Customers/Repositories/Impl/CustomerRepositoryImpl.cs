using System.Linq;
using NH.Data.Customers.Config;
using NH.Data.Customers.Models;

namespace NH.Data.Customers.Repositories.Impl
{
    public class CustomerRepositoryImpl : CustomerRepository
    {
        private readonly PersistenceBroker<CustomersConfiguration> _persistenceBroker;

        public CustomerRepositoryImpl(PersistenceBroker<CustomersConfiguration> persistenceBroker)
        {
            _persistenceBroker = persistenceBroker;
        }

        public Customer FindById(int id)
        {
            return _persistenceBroker.Query<Customer>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}