using System.Linq;
using FluentAssertions;
using NH.Data.Northwind.Repositories;
using NUnit.Framework;

namespace NH.Data.Northwind.Tests.Integration
{
    class CustomerRepositoryTest : TransactionalNorthwindTest<CustomerRepository>
    {
        [Test]
        public void ItCanFindHandfulsAtATime()
        {
            Repository.FindFirst(2).Select(x => x.CompanyName)
                .Should().Equal(new[] { "Alfreds Futterkiste", "Ana Trujillo Emparedados y helados" });
        }
    }
}
