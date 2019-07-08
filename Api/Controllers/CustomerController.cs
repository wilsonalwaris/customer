using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private DbContextOptions<CustomerContext> DbOptions;
        private CustomerRepository customerRepository;

        public CustomerController()
        {
            this.DbOptions = SqliteHelper.GetDatabaseOptions();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            Customer customer = new Customer();
            using (var customerContext = new CustomerContext(this.DbOptions))
            {
                var customerRepository = new CustomerRepository(customerContext);
                customer = customerRepository.GetCustomer(id);
            }
            
            return customer;
        }
    }
}
