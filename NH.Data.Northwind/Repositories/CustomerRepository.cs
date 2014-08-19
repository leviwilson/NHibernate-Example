using System.Collections.Generic;
using NH.Data.Northwind.Models;

namespace NH.Data.Northwind.Repositories
{
    public interface CustomerRepository
    {
        IList<Customer> FindFirst(int howMany);
    }
}
