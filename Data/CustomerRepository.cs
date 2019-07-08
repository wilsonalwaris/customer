using System;
using System.Linq;

namespace Data
{
    public class CustomerRepository : IDisposable
    {
        private CustomerContext customerContext;

        public CustomerRepository(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return;
                }

                this.customerContext.Customer.Add(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer GetCustomer(int customerId)
        {
            return this.customerContext.Customer.FirstOrDefault(cust => cust.Id == customerId);
        }

        public bool SaveChanges()
        {
            try
            {
                return this.customerContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (this.customerContext == null)
            {
                return;
            }

            this.customerContext = null;
        }
    }
}
