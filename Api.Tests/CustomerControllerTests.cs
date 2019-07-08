using Api;
using Api.Tests2;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class CustomerControllerTests
    {
        private WebApplicationFactory<Startup> webApplicationFactory;
        private CustomerRepository customerRepository;
        private DbContextOptions<CustomerContext> testDbOptions;

        [SetUp]
        public void Setup()
        {
            this.webApplicationFactory = new ApiWebApplicationFactory();
            this.testDbOptions = SqliteHelper.GetDatabaseOptions();
        }

        [Test]
        public async Task GetCustomerTest()
        {
            // arrange
            var client = this.webApplicationFactory.CreateClient();
            var customerToBeAddedToDb = new Customer
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "janedoe@jd.com",
                Password = "********"
            };

            using (var customerContext = new CustomerContext(this.testDbOptions))
            {
                if (SqliteHelper.DatabaseDoesNotExist(customerContext))
                {
                    Assert.Fail();
                }

                this.customerRepository = new CustomerRepository(customerContext);
                this.customerRepository.AddCustomer(customerToBeAddedToDb);
                this.customerRepository.SaveChanges();
            }

            // Act 
            var response = await client.GetAsync("/api/customer/1");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}