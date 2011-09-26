using FluentNHibernate.Cfg;

namespace NH.Data.Tests
{
    public class TestConfiguration : SessionConfiguration
    {
        public string Dialect
        {
            get { throw new System.NotImplementedException(); }
        }

        public string ConnectionDriver
        {
            get { throw new System.NotImplementedException(); }
        }

        public string ConnectionString
        {
            get { throw new System.NotImplementedException(); }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            throw new System.NotImplementedException();
        }
    }

    class TestTwoConfiguration : TestConfiguration
    {
    }
}