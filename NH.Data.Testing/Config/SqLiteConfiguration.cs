using FluentNHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NH.Data.Testing.Config
{
    public class SqLiteConfiguration<TConfig> : SessionConfiguration where TConfig : SessionConfiguration, new()
    {
        private readonly TConfig _wrappedConfig;

        public SqLiteConfiguration()
        {
            _wrappedConfig = new TConfig();
        }

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
            get { return "Data Source=:memory:;Version=3;New=True;"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            _wrappedConfig.ConfigureMappings(mappingConfiguration);
        }
    }
}