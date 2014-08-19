using System.Linq;
using NH.Data.Config;
using NH.Data.Testing.Config;
using NHibernate;
using NUnit.Framework;
using Ninject;
using Ninject.Modules;

namespace NH.Data.Testing
{
    public abstract class InMemoryDatabaseTest<TDatabase> where TDatabase : SessionConfiguration, new()
    {
        private readonly StandardKernel _kernel;
        protected UnitOfWork UnitOfWork { get; set; }

        protected InMemoryDatabaseTest(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(new[] { new NhDataModule() }.Concat(modules).ToArray());
        }

        [SetUp]
        public void Before()
        {
            _kernel.Inject(this);

            // setup the in-memory context
            var sessionFactoryContext = _kernel.Get<SessionFactoryContext>();
            sessionFactoryContext.Get<SqLiteConfiguration<TDatabase>>(InMemoryDatabaseBuilder.Intercept);

            // alias the real one to the in-memory one
            sessionFactoryContext.Alias<TDatabase, SqLiteConfiguration<TDatabase>>();

            // create the connection
            UnitOfWork = _kernel.Get<UnitOfWorkFactory>().StartUnitOfWork<TDatabase>();
            InMemoryDatabaseBuilder.Build(Session);
        }

        [TearDown]
        public void After()
        {
            UnitOfWork.Dispose();
        }

        protected T Create<T>(T model)
        {
            return (T)PersistenceBroker.Create(model);
        }

        protected T Create<T>() where T : new()
        {
            return Create(new T());
        }

        protected TEntity CreateAndRefresh<TEntity>() where TEntity : new()
        {
            var entity = Create<TEntity>();
            Refresh(entity);
            return entity;
        }

        protected TModel Find<TModel>(object id)
        {
            return PersistenceBroker.Get<TModel>(id);
        }

        protected PersistenceBroker<TDatabase> PersistenceBroker
        {
            get { return Get<PersistenceBroker<TDatabase>>(); }
        }

        protected T Get<T>()
        {
            return _kernel.Get<T>();
        }

        protected void Refresh(object entity)
        {
            UnitOfWork.Refresh(entity);
        }

        protected void ClearNHibernateCache()
        {
            UnitOfWork.Clear();
        }

        protected void SubmitNHibernateChanges()
        {
            UnitOfWork.Flush();
        }

        private static ISession Session
        {
            get { return SessionDataContext.Get<TDatabase>(); }
        }
    }
}
