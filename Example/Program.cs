using System;
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
            var kernel = new StandardKernel(new NhDataModule(), new CustomerModule());

            using (kernel.Get<UnitOfWorkFactory>().StartUnitOfWork<CustomersConfiguration>())
            {
                var customerRepository = kernel.Get<CustomerRepository>();

                var customer = customerRepository.FindById(1);

                if( null == customer)
                    Console.WriteLine("Customer not found.");
                else
                {
                    Console.WriteLine("Found customer {0}!", customer.CustomerName);
                }
            }
        }
    }
}
