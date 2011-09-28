using FluentNHibernate.Mapping;

namespace NH.Data.Northwind.Models.Mappings
{
    sealed class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("Customers");

            Id(x => x.CustomerId).Column("[Customer ID]").GeneratedBy.Assigned();

            Map(x => x.CompanyName).Column("[Company Name]");
            Map(x => x.ContactName).Column("[Contact Name]");
            Map(x => x.ContactTitle).Column("[Contact Title]");
            Map(x => x.Address).Column("[Address]");
            Map(x => x.City).Column("[City]");
            Map(x => x.Region).Column("[Region]");
            Map(x => x.PostalCode).Column("[Postal Code]");
            Map(x => x.Country).Column("[Country]");
            Map(x => x.Phone).Column("[Phone]");
            Map(x => x.Fax).Column("[Fax]");
        }
    }
}