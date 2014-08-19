using NH.Data.Northwind.Config;
using NH.Data.Testing;

namespace NH.Data.Northwind.Tests
{
    public abstract class TransactionalNorthwindTest<TRepository> : TransactionalRepositoryTest<Config.Northwind, TRepository> where TRepository : class
    {
        protected TransactionalNorthwindTest() : base(new NorthwindModule())
        {
            Configuration.ConnectionString = @"Data Source=..\..\..\Example\Northwind.sdf";
        }
    }
}
