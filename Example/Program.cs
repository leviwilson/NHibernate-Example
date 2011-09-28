using System;
using System.Linq;
using NH.Data.Northwind.Config;
using NH.Data.Northwind.Repositories;
using Ninject;
using NH.Data;
using NH.Data.Config;
using NH.Data.Customers.Config;
using NH.Data.Customers.Repositories;

namespace Example
{
    class Program
    {
        static void Main()
        {
            var kernel = new StandardKernel(new NhDataModule(), new RestaurantModule(), new NorthwindModule());

            var unitOfWorkFactory = kernel.Get<UnitOfWorkFactory>();

            using (unitOfWorkFactory.StartUnitOfWork<RestaurantConfiguration>())
            using (unitOfWorkFactory.StartUnitOfWork<Northwind>())
            {
                FindARestaurant(kernel);
                FindSomeCustomers(kernel);
            }
        }

        private static void FindARestaurant(StandardKernel kernel)
        {
            var restaurant = kernel.Get<RestaurantRepository>()
                .FindById(1);

            if (null == restaurant)
                Console.WriteLine("Restaurant not found.\n");
            else
                Console.WriteLine("Found customer {0}!\n", restaurant.RestaurantName);
        }

        private static void FindSomeCustomers(StandardKernel kernel)
        {
            foreach (var customer in kernel.Get<CustomerRepository>().FindFirst(25))
                Console.WriteLine("{0,-40}{1,-15}", customer.CompanyName, customer.City);
        }
    }
}
