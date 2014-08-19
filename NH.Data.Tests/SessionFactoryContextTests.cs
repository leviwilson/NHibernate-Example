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

        [TearDown]
        public void TearDown()
        {
            _sessionFactoryContext.Dispose();
        }

        [Test]
        public void Then_session_factories_are_only_initialized_once()
        {
            var expectedSessionFactory = _sessionFactoryContext.Get<ArtistConfiguration>();
            
            expectedSessionFactory
                .Should().Not.Be.Null();

            _sessionFactoryContext.Get<ArtistConfiguration>()
                .Should().Equal(expectedSessionFactory);
        }


        [Test]
        public void Then_session_factories_can_be_aliased()
        {
            _sessionFactoryContext.Alias<ArtistConfiguration, InMemoryTestConfiguration<ArtistConfiguration>>();

            _sessionFactoryContext.Get<ArtistConfiguration>().Should()
                .Be.SameAs(_sessionFactoryContext.Get<InMemoryTestConfiguration<ArtistConfiguration>>());
        }

    }
}
