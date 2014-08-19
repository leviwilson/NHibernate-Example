using Ninject.Modules;

namespace NH.Data.Testing
{
    public abstract class InMemoryRepositoryTest<TDatabase, TRepository> : InMemoryDatabaseTest<TDatabase> where TRepository : class where TDatabase : SessionConfiguration, new()
    {
        private TRepository _repository;

        protected InMemoryRepositoryTest(params INinjectModule[] modules) : base(modules)
        { }

        protected TRepository Repository
        {
            get { return _repository ?? (_repository = Get<TRepository>()); }
        }
    }
}
