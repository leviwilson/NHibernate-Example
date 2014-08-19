using System;
using NH.Data.Impl;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Environment = NHibernate.Cfg.Environment;

namespace NH.Data.Tests
{
    public class InMemorySQLiteTestBase
    {
        private SessionFactoryContext _sessionFactoryContext;
        private ISessionFactory _sessionFactory;
        private Configuration _interceptedConfiguration;
        
        protected UnitOfWorkImpl _unitOfWork;
        protected ISession _session;

        [SetUp]
        public void SetUp()
        {
            _sessionFactoryContext = new SessionFactoryContext();
            _sessionFactory = _sessionFactoryContext.Get<ArtistConfiguration>(SetupInMemoryConfiguration());

            _session = _sessionFactory.OpenSession();

            InitializeInMemoryDatabase();

            _unitOfWork = new UnitOfWorkImpl(_session);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Dispose();
            _sessionFactory.Dispose();
            _sessionFactoryContext.Dispose();
        }


        private void InitializeInMemoryDatabase()
        {
            new SchemaExport(_interceptedConfiguration)
                .Execute(false, true, false, _session.Connection, null);
        }

        private Action<Configuration> SetupInMemoryConfiguration()
        {
            return x =>
                       {
                           _interceptedConfiguration = x;
                           _interceptedConfiguration.SetProperty(Environment.ReleaseConnections, "on_close");
                       };
        }
    }
}