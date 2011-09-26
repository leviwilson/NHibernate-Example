using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NH.Data.Tests
{
    public class InMemoryTestConfiguration : SessionConfiguration
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
            get { return "Data Source=:memory:;Version=3;New=True;"; }
        }

        public void ConfigureMappings(MappingConfiguration mappingConfiguration)
        {
            mappingConfiguration.FluentMappings
                .Add<ArtistMap>();
        }
    }

    class InMemoryTestTwoConfiguration : InMemoryTestConfiguration
    {
    }

    internal class Artist
    {
        public virtual int Id { get; set; }
        public virtual string ArtistName { get; set; }
    }

    internal class ArtistMap : ClassMap<Artist>
    {
        public ArtistMap()
        {
            Table("Artists");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ArtistName);
        }
    }
}