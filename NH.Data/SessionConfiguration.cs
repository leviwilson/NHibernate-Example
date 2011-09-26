using FluentNHibernate.Cfg;
using NHibernate.Cfg;

namespace NH.Data
{
    public interface SessionConfiguration
    {
        string Dialect { get; }
        string ConnectionDriver { get; }
        string ConnectionString { get; }
        void ConfigureMappings(MappingConfiguration mappingConfiguration);
    }

    public static class SessionConfigurationExtension
    {
        public static Configuration ToConfiguration(this SessionConfiguration sessionConfiguration)
        {
            var configuration = new Configuration();
            configuration.SetProperty(Environment.Dialect, sessionConfiguration.Dialect);
            configuration.SetProperty(Environment.ConnectionDriver, sessionConfiguration.ConnectionDriver);
            configuration.SetProperty(Environment.ConnectionString, sessionConfiguration.ConnectionString);

            return configuration;
        }
    }
}