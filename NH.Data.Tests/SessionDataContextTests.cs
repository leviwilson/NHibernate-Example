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
            var session = SessionDataContext.Get<InMemoryTestConfiguration>();
            session.Should().Be.Null();
        }

        [Test]
        public void Then_a_session_can_be_set_and_returned()
        {
            var expectedSession = MockRepository.GenerateMock<ISession>();

            SessionDataContext.Set<InMemoryTestConfiguration>(expectedSession);

            var actualSession = SessionDataContext.Get<InMemoryTestConfiguration>();
            actualSession.Should().Equal(expectedSession);
        }

        [Test]
        public void Then_multiple_sessions_can_be_saved()
        {
            var sessionOne = MockRepository.GenerateMock<ISession>();
            var sessionTwo = MockRepository.GenerateMock<ISession>();

            SessionDataContext.Set<InMemoryTestConfiguration>(sessionOne);
            SessionDataContext.Set<InMemoryTestTwoConfiguration>(sessionTwo);

            SessionDataContext.Get<InMemoryTestConfiguration>()
                .Should().Equal(sessionOne);

            SessionDataContext.Get<InMemoryTestTwoConfiguration>()
                .Should().Equal(sessionTwo);
        }

        [Test]
        public void Then_different_threads_have_different_contexts()
        {
            var threadOneSession = MockRepository.GenerateMock<ISession>();

            SessionDataContext.Set<InMemoryTestConfiguration>(threadOneSession);

            ISession threadTwoSession = null;

            var threadTwo = new Thread(() => threadTwoSession = SessionDataContext.Get<InMemoryTestConfiguration>());
            threadTwo.Start();
            threadTwo.Join();

            SessionDataContext.Get<InMemoryTestConfiguration>()
                .Should().Equal(threadOneSession);

            threadTwoSession.Should().Be.Null();
        }
    }
}
