using System.Collections.Generic;
using System.Linq;
using NH.Data.Northwind.Models;

namespace NH.Data.Northwind.Repositories.Impl
{
    public class CustomerRepositoryImpl : CustomerRepository
    {
        private readonly PersistenceBroker<Config.Northwind> _persistenceBroker;

        public CustomerRepositoryImpl(PersistenceBroker<Config.Northwind> persistenceBroker)
        {
            _persistenceBroker = persistenceBroker;
        }


        public IList<Customer> FindFirst(int howMany)
        {
            return _persistenceBroker.Query<Customer>()
                .Take(howMany).ToList();
        }
    }
}