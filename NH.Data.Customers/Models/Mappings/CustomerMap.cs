using FluentNHibernate.Mapping;

namespace NH.Data.Customers.Models.Mappings
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("customers");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.CustomerName).Column("Name");
        }
    }
}