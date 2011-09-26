using System.Data;
using System.Linq;
using NH.Data.Impl;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Should.Fluent;

namespace NH.Data.Tests
{
    [TestFixture]
    public class WhenWorkingWithAUnitOfWork
    {
        private SessionFactoryContext _sessionFactoryContext;
        private ISessionFactory _sessionFactory;
        private UnitOfWorkImpl _unitOfWork;
        private ISession _session;

        [SetUp]
        public void SetUp()
        {
            _sessionFactoryContext = new SessionFactoryContext();
            _sessionFactory = _sessionFactoryContext.Get<InMemoryTestConfiguration>();

            _session = _sessionFactory.OpenSession();
            _unitOfWork = new UnitOfWorkImpl(_session);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Dispose();
            _sessionFactory.Dispose();
        }

        [Test]
        [Ignore("SQLite In-Memory database not working")]
        public void Then_we_can_commit_transactions()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);

            var newArtist = new Artist {ArtistName = "The Who"};
            _session.Save(newArtist);

            _unitOfWork.Commit();

            var artistCount = _session.Query<Artist>().Count();
            artistCount.Should().Equal(1);
        }
    }
}
