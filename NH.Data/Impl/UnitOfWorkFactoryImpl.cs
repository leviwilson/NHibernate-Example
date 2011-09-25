namespace NH.Data.Impl
{
    public class UnitOfWorkFactoryImpl : UnitOfWorkFactory
    {
        private readonly SessionFactoryContext _sessionFactoryContext;

        public UnitOfWorkFactoryImpl(SessionFactoryContext sessionFactoryContext)
        {
            _sessionFactoryContext = sessionFactoryContext;
        }

        public UnitOfWork StartUnitOfWork<T>() where T : SessionConfiguration
        {
            var session = _sessionFactoryContext.Get<T>()
                .OpenSession();

            SessionDataContext.Set<T>(session);

            return new UnitOfWorkImpl(session);
        }
    }
}