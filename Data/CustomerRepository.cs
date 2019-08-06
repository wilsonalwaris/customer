using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerContext customerContext;
        private readonly ICustomerRepositoryHelper customerRepositoryHelper;

        public CustomerRepository(
            CustomerContext customerContext,
            ICustomerRepositoryHelper customerRepositoryHelper)
        {
            this.customerContext = customerContext;
            this.customerRepositoryHelper = customerRepositoryHelper;
        }

        public bool AddCustomer(Customer customer)
        {
            if (this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext) || customer == null)
            {
                return false;
            }

            if (this.customerContext.Customer.Contains(customer))
            {
                return true;
            }

            this.customerContext.Customer.Add(customer);
            this.SaveChanges();
            return true;
        }

        public Customer GetCustomer(int customerId)
        {
            if (this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext))
            {
                return null;
            }

            return this.customerContext.Customer.First(cust => cust.Id == customerId);
        }

        public bool DeleteCustomer(int customerId)
        {
            if (this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext))
            {
                return false;
            }


            if (!this.customerContext.Customer.Any(customer => customer.Id == customerId))
            {
                return false;
            }

            var customerToDelete = this.customerContext.Customer.First(customer => customer.Id == customerId);
            this.customerContext.Customer.Remove(customerToDelete);
            this.SaveChanges();
            return true;
        }

        public bool ContainsCustomer(Customer customer)
        {
            if (this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext) || customer == null)
            {
                return false;
            }

            return this.customerContext.Customer.Contains(customer);
        }

        private bool SaveChanges()
        {
           return this.customerContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            if (this.customerContext != null)
            {
                this.customerContext = null;
            }
        }
    }
}
