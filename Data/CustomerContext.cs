using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            :base(options)
        { }
    }
}
