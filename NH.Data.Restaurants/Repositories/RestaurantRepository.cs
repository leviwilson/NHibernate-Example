using NH.Data.Customers.Models;

namespace NH.Data.Customers.Repositories
{
    public interface RestaurantRepository
    {
        Restaurant FindById(int id);
    }
}
