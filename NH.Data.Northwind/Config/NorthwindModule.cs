using NH.Data.Northwind.Repositories;
using NH.Data.Northwind.Repositories.Impl;
using Ninject.Modules;

namespace NH.Data.Northwind.Config
{
    public class NorthwindModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CustomerRepository>().To<CustomerRepositoryImpl>();
        }
    }
}
