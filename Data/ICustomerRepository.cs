namespace Data
{
    public interface ICustomerRepository
    {
        bool AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        bool DeleteCustomer(int customerId);
        bool ContainsCustomer(Customer customer);
    }
}