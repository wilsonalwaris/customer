using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface ICustomerRepositoryHelper
    {
        bool DatabaseDoesNotExist(CustomerContext customerContext);
    }
}