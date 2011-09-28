using FluentNHibernate.Mapping;

namespace NH.Data.Customers.Models.Mappings
{
    public sealed class RestaurantsMap : ClassMap<Restaurant>
    {
        public RestaurantsMap()
        {
            Table("restaurants");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.RestaurantName).Column("Name");
        }
    }
}