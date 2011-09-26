using System.Threading;
using NHibernate;
using NUnit.Framework;
using Should.Fluent;

namespace NH.Data.Tests
{
    [TestFixture]
    public class WhenUsingTheSessionFactoryContext
    {
        private SessionFactoryContext _sessionFactoryContext;

        [SetUp]
        public void SetUp()
        {
            _sessionFactoryContext = new SessionFactoryContext();
        }

        [Test]
        public void Then_session_factories_are_only_initialized_once()
        {
            var expectedSessionFactory = _sessionFactoryContext.Get<InMemoryTestConfiguration>();
            
            expectedSessionFactory
                .Should().Not.Be.Null();

            _sessionFactoryContext.Get<InMemoryTestConfiguration>()
                .Should().Equal(expectedSessionFactory);
        }

        [Test]
        public void Then_session_factories_will_be_different_across_threads()
        {
            var threadOneSessionFactory = _sessionFactoryContext.Get<InMemoryTestConfiguration>();

            ISessionFactory threadTwoSessionFactory = null;
            var threadTwo = new Thread(() => threadTwoSessionFactory = _sessionFactoryContext.Get<InMemoryTestConfiguration>());
            threadTwo.Start();
            threadTwo.Join();

            threadTwoSessionFactory
                .Should().Not.Equal(threadOneSessionFactory);
        }
    }
}
