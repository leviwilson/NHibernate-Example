using FluentNHibernate.Cfg;

namespace NH.Data
{
    public interface SessionConfiguration
    {
        string Dialect { get; }
        string ConnectionDriver { get; }
        string ConnectionString { get; }
        void ConfigureMappings(MappingConfiguration mappingConfiguration);
    }
}