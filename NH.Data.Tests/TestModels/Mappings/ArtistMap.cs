using FluentNHibernate.Mapping;

namespace NH.Data.Tests.TestModels.Mappings
{
    internal sealed class ArtistMap : ClassMap<Artist>
    {
        public ArtistMap()
        {
            Table("Artists");

            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ArtistName);
        }
    }
}