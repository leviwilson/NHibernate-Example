using Ninject.Modules;
using NH.Data.Customers.Repositories;
using NH.Data.Customers.Repositories.Impl;

namespace NH.Data.Customers.Config
{
    public class CustomerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CustomerRepository>()
                .To<CustomerRepositoryImpl>();
        }
    }
}