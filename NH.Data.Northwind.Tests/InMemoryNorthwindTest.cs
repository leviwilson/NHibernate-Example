using NH.Data.Northwind.Config;
using NH.Data.Testing;

namespace NH.Data.Northwind.Tests
{
    public abstract class InMemoryNorthwindTest : InMemoryDatabaseTest<Config.Northwind>
    {
        protected InMemoryNorthwindTest() : base(new NorthwindModule())
        {}
    }
}
