using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class CustomerContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<Customer> Customer { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            :base(options)
        { }
    }
}
