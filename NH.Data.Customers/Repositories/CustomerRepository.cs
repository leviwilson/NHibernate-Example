using NH.Data.Customers.Models;

namespace NH.Data.Customers.Repositories
{
    public interface CustomerRepository
    {
        Customer FindById(int id);
    }
}
