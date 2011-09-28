using FluentNHibernate.Cfg;
using NH.Data.Northwind.Models.Mappings;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NH.Data.Northwind.Config
{
    public class Northwind : SessionConfiguration
    {
        public string Dialect
        {
            get { return typeof(MsSqlCe40Dialect).AssemblyQualifiedName; }
        }

        public string ConnectionDriver
        {
            get { return typeof(SqlServerCeDriver).AssemblyQualifiedName; }
        }

        public string ConnectionString
        {
            get { return "Data Source=Northwind.sdf"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            mappingConfiguration.FluentMappings
                .Add<CustomerMap>();
        }
    }
}
