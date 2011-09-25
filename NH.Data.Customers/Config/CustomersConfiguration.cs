using FluentNHibernate.Cfg;
using NH.Data.Customers.Models.Mappings;

namespace NH.Data.Customers.Config
{
    public class CustomersConfiguration : SessionConfiguration
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
            get { return "Data Source=customers.db;Version=3;"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            mappingConfiguration.FluentMappings
                .Add<CustomerMap>();
        }
    }
}
