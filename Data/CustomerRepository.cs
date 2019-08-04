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

            if (!this.customerContext.Customer.Contains(customer))
            {
                this.customerContext.Customer.Add(customer);
                this.SaveChanges();
            }
            
            return true;
        }

        public Customer GetCustomer(int customerId)
        {
            if (this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext))
            {
                return null;
            }

            if (this.customerContext.Customer == null || !this.customerContext.Customer.Any())
            {
                return null;
            }

            return this.customerContext.Customer.First(cust => cust.Id == customerId);
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
