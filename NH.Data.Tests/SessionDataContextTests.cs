using System.Threading;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Should.Fluent;

namespace NH.Data.Tests
{
    [TestFixture]
    public class WhenUsingSessionDataContext
    {
        [SetUp]
        public void SetUp()
        {
            SessionDataContext.Reset();
        }

        [Test]
        public void Then_null_is_returned_if_a_session_has_not_been_set()
        {
            var session = SessionDataContext.Get<ArtistConfiguration>();
            session.Should().Be.Null();
        }

        [Test]
        public void Then_a_session_can_be_set_and_returned()
        {
            var expectedSession = MockRepository.GenerateMock<ISession>();

            SessionDataContext.Set<ArtistConfiguration>(expectedSession);

            var actualSession = SessionDataContext.Get<ArtistConfiguration>();
            actualSession.Should().Equal(expectedSession);
        }

        [Test]
        public void Then_multiple_sessions_can_be_saved()
        {
            var sessionOne = MockRepository.GenerateMock<ISession>();
            var sessionTwo = MockRepository.GenerateMock<ISession>();

            SessionDataContext.Set<ArtistConfiguration>(sessionOne);
            SessionDataContext.Set<InMemoryTestTwoConfiguration>(sessionTwo);

            SessionDataContext.Get<ArtistConfiguration>()
                .Should().Equal(sessionOne);

            SessionDataContext.Get<InMemoryTestTwoConfiguration>()
                .Should().Equal(sessionTwo);
        }

        [Test]
        public void Then_different_threads_have_different_contexts()
        {
            var threadOneSession = MockRepository.GenerateMock<ISession>();

            SessionDataContext.Set<ArtistConfiguration>(threadOneSession);

            ISession threadTwoSession = null;

            var threadTwo = new Thread(() => threadTwoSession = SessionDataContext.Get<ArtistConfiguration>());
            threadTwo.Start();
            threadTwo.Join();

            SessionDataContext.Get<ArtistConfiguration>()
                .Should().Equal(threadOneSession);

            threadTwoSession.Should().Be.Null();
        }
    }
}
