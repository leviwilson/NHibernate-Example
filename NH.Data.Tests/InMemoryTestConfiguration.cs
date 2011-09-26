using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;

namespace NH.Data.Tests
{
    public class InMemoryTestConfiguration : SessionConfiguration
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
        public static string TableCreate = "create table artists (id integer primary key, artistname varchar(100))";

        public ArtistMap()
        {
            Table("Artist");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ArtistName);
        }
    }
}