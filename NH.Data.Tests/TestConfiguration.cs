using FluentNHibernate.Cfg;

namespace NH.Data.Tests
{
    public class TestConfiguration : SessionConfiguration
    {
        public string Dialect
        {
            get { return "NHibernate.Dialect.SQLiteDialect"; }
        }

        public string ConnectionDriver
        {
            get { return "NHibernate.Driver.SQLite20Driver"; }
        }

        public string ConnectionString
        {
            get { return "Data Source=:memory:;Version=3;New=True;"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
        }
    }

    class TestTwoConfiguration : TestConfiguration
    {
    }
}