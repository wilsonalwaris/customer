using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Register
{
    public class DependencyRegister : IDependencyRegister
    {
        public void LoadServices(IServiceCollection serviceCollection)
        {
            var databaseHelper = new DatabaseHelper();
            serviceCollection.AddDbContext<CustomerContext>(options => options.UseSqlite(databaseHelper.GetConnection()));
            serviceCollection.AddTransient<ICustomerRepositoryHelper, CustomerRepositoryHelper>();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
