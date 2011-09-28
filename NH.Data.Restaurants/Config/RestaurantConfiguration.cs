using FluentNHibernate.Cfg;
using NH.Data.Customers.Models.Mappings;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NH.Data.Customers.Config
{
    public class RestaurantConfiguration : SessionConfiguration
    {
        public string Dialect
        {
            get { return typeof(SQLiteDialect).AssemblyQualifiedName; }
        }

        public string ConnectionDriver
        {
            get { return typeof(SQLite20Driver).AssemblyQualifiedName; }
        }

        public string ConnectionString
        {
            get { return "Data Source=restaurants.db;Version=3;"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            mappingConfiguration.FluentMappings
                .Add<RestaurantsMap>();
        }
    }
}
