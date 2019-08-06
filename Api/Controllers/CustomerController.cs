using Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            return this.customerRepository.GetCustomer(id);
        }

        [HttpPost]
        public void Add(Customer customer)
        {
            if (customer != null)
            {
                this.customerRepository.AddCustomer(customer);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           this.customerRepository.DeleteCustomer(id);
        }
    }
}
