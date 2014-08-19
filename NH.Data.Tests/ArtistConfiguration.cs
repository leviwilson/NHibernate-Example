using FluentNHibernate.Cfg;
using NH.Data.Tests.TestModels.Mappings;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NH.Data.Tests
{
    public class ArtistConfiguration : SessionConfiguration
    {

        public string Dialect
        {
            get { return typeof(MsSql2008Dialect).AssemblyQualifiedName; }
        }

        public string ConnectionDriver
        {
            get { return typeof(SqlClientDriver).AssemblyQualifiedName; }
        }

        public string ConnectionString
        {
            get { return @"data source=.\sqlexpress;initial catalog=CompassFramework;user id=sa;password=northwoods;"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            mappingConfiguration.FluentMappings
                                .Add<ArtistMap>();
        }
    }
}