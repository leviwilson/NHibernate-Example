using System;
using System.Data;
using System.Linq;
using NH.Data.Impl;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Should.Fluent;
using Environment = NHibernate.Cfg.Environment;

namespace NH.Data.Tests
{
    [TestFixture]
    public class WhenWorkingWithAUnitOfWork
    {
        private SessionFactoryContext _sessionFactoryContext;
        private ISessionFactory _sessionFactory;
        private UnitOfWorkImpl _unitOfWork;
        private ISession _session;
        private Configuration _interceptedConfiguration;

        [SetUp]
        public void SetUp()
        {
            _sessionFactoryContext = new SessionFactoryContext();
            _sessionFactory = _sessionFactoryContext.Get<InMemoryTestConfiguration>(SetupInMemoryConfiguration());

            _session = _sessionFactory.OpenSession();

            InitializeInMemoryDatabase();

            _unitOfWork = new UnitOfWorkImpl(_session);
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

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Dispose();
            _sessionFactory.Dispose();
            _sessionFactoryContext.Dispose();
        }

        [Test]
        public void Then_we_can_commit_transactions()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);

            var newArtist = new Artist {ArtistName = "The Who"};
            _session.Save(newArtist);

            _unitOfWork.Commit();

            var artists = _session.Query<Artist>().ToList();

            artists.Count()
                .Should().Equal(1);

            var actualArtist = artists.First();

            actualArtist
                .Id.Should().Equal(1);

            actualArtist
                .ArtistName.Should().Equal("The Who");
        }

        [Test]
        public void Then_we_can_rollback_transactions()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);

            var newArtist = new Artist {ArtistName = "The Who"};
            _session.Save(newArtist);

            newArtist.Id
                .Should().Not.Equal(0);

            _unitOfWork.Rollback();

            _session.Query<Artist>().Count()
                .Should().Equal(0);
        }
    }
}
