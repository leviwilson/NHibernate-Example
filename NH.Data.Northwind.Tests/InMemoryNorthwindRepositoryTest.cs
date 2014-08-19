using NH.Data.Northwind.Config;
using NH.Data.Testing;

namespace NH.Data.Northwind.Tests
{
    public abstract class InMemoryNorthwindRepositoryTest<TRepository> : InMemoryRepositoryTest<Config.Northwind, TRepository> where TRepository : class
    {
        protected InMemoryNorthwindRepositoryTest() : base(new NorthwindModule())
        { }
    }
}