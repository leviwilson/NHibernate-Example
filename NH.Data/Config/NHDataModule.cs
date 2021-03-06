using Ninject.Modules;
using NH.Data.Impl;

namespace NH.Data.Config
{
    public class NhDataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UnitOfWorkFactory>()
                .To<UnitOfWorkFactoryImpl>()
                .InSingletonScope();

            Bind(typeof(PersistenceBroker<>))
                .To(typeof(PersistenceBrokerImpl<>))
                .InSingletonScope();
        }
    }
}