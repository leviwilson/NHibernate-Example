using System;
using System.Data;
using System.Linq;
using NH.Data.Config;
using NUnit.Framework;
using Ninject;
using Ninject.Modules;

namespace NH.Data.Testing
{
    public abstract class TransactionalRepositoryTest<TDatabase, TRepository> where TRepository : class where TDatabase : SessionConfiguration, new()
    {
        protected UnitOfWork UnitOfWork;

        private TRepository _repository;
        private readonly StandardKernel _kernel;

        protected TransactionalRepositoryTest(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(new[] {new NhDataModule()}.Concat(modules).ToArray());
        }

        [SetUp]
        protected void SetUp()
        {
            _kernel.Inject(this);

            Get<SessionFactoryContext>().Get<TDatabase>((c) => c.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true"));

            var unitOfWorkFactory = _kernel.Get<UnitOfWorkFactory>();

            UnitOfWork = unitOfWorkFactory.StartUnitOfWork<TDatabase>();
            UnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        [TearDown]
        protected void TearDown()
        {
            UnitOfWork.Dispose();
        }

        protected TRepository Repository
        {
            get { return _repository ?? (_repository = _kernel.Get<TRepository>()); }
        }

        public TImpl Get<TImpl>()
        {
            return _kernel.Get<TImpl>();
        }

        protected TModel Create<TModel>(TModel model)
        {
            return (TModel)PersistenceBroker.Create(model);
        }

        protected TModel Find<TModel>(int id)
        {
            return PersistenceBroker.Get<TModel>(id);
        }

        protected TDatabase Configuration
        {
            get { return _kernel.Get<TDatabase>(); }
        }

        protected PersistenceBroker<TDatabase> PersistenceBroker
        {
            get { return Get<PersistenceBroker<TDatabase>>(); }
        }

        protected object Prefetch<TEntity>(TEntity entity, Func<TEntity, object> fetchFor)
        {
            return fetchFor(entity);
        }
    }

}
