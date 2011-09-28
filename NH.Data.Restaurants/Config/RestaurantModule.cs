using Ninject.Modules;
using NH.Data.Customers.Repositories;
using NH.Data.Customers.Repositories.Impl;

namespace NH.Data.Customers.Config
{
    public class RestaurantModule : NinjectModule
    {
        public override void Load()
        {
            Bind<RestaurantRepository>()
                .To<RestaurantRepositoryImpl>()
                .InSingletonScope();
        }
    }
}