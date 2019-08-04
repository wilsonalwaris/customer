namespace Data
{
    public interface ICustomerRepository
    {
        bool AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
    }
}