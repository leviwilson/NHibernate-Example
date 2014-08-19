using System.Linq;
using FluentAssertions;
using NH.Data.Northwind.Models;
using NH.Data.Northwind.Repositories;
using NH.Data.Testing;
using NUnit.Framework;

namespace NH.Data.Northwind.Tests.Units
{
    class CustomerRepositoryUnit : InMemoryNorthwindRepositoryTest<CustomerRepository>
    {
        [Test]
        public void FindsTheFirstHandful()
        {
            10.Times(x =>
                {
                    var customer = new Customer {CustomerId = string.Format("CUST{0:000}", x)};
                    Create(customer);
                });
            ClearNHibernateCache();

            Repository.FindFirst(3).Select(x => x.CompanyName)
                      .Should().Equal(new[] {"Company 1", "Company 2", "Company 3"});
        }
    }
}
