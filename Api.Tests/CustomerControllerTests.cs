using Api;
using Api.Tests;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Tests
{
    public class CustomerControllerTests : IDisposable
    {
        private WebApplicationFactory<Startup> webApplicationFactory;
        private ICustomerRepository customerRepository;
        private DbContextOptions<CustomerContext> dbOptions;
        private CustomerContext customerContext;
        private ICustomerRepositoryHelper customerRepositoryHelper;
        private IDatabaseHelper databaseHelper;

        [SetUp]
        public void Setup()
        {
            this.webApplicationFactory = new ApiWebApplicationFactory();
            this.customerRepositoryHelper = new CustomerRepositoryHelper();
            this.databaseHelper = new DatabaseHelper();

            this.dbOptions = this.databaseHelper.GetDatabaseOptions();
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

            this.customerContext = new CustomerContext(this.dbOptions);
            this.customerRepository = new CustomerRepository(this.customerContext, this.customerRepositoryHelper);
            this.customerRepository.AddCustomer(customerToBeAddedToDb);

            // Act 
            var response = await client.GetAsync("/api/customer/1");
            var customerRetrievedFromDb = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.AreEqual(customerToBeAddedToDb.Id, customerRetrievedFromDb.Id);
        }

        public void Dispose()
        {
            this.customerContext = null;
        }
    }
}