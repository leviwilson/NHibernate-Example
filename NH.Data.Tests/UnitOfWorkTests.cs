using System.Data;
using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;
using Should.Fluent;

namespace NH.Data.Tests
{
    [TestFixture]
    public class WhenWorkingWithAUnitOfWork : InMemorySQLiteTestBase
    {
        [Test]
        public void Then_we_can_commit_transactions()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);

            var newArtist = new Artist {ArtistName = "The Who"};
            _session.Save(newArtist);

            _unitOfWork.Commit();

            _session.Query<Artist>()
                .Count().Should().Equal(1);
        }

        [Test]
        public void Then_we_can_rollback_transactions()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);

            var newArtist = new Artist {ArtistName = "The Who"};
            _session.Save(newArtist);

            _unitOfWork.Rollback();

            _session.Query<Artist>().Count()
                .Should().Equal(0);
        }

        [Test]
        public void Then_we_will_only_rollback_a_transaction_if_one_has_been_started()
        {
            Assert.DoesNotThrow(() => _unitOfWork.Rollback());
        }

        [Test]
        public void Then_you_can_only_rollback_a_transaction_once()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
            _unitOfWork.Rollback();
            Assert.DoesNotThrow(() => _unitOfWork.Rollback());
        }

        [Test]
        public void Then_we_will_only_commit_an_active_transaction()
        {
            Assert.DoesNotThrow(() => _unitOfWork.Commit());
        }

        [Test]
        public void Then_we_can_only_commit_a_transaction_once()
        {
            _unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
            _unitOfWork.Commit();
            Assert.DoesNotThrow(() => _unitOfWork.Commit());
        }
    }
}
