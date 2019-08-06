using Api;
using Api.Tests;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
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
        private HttpClient httpClient;
        private Customer customerToBeAdded;

        [SetUp]
        public void Setup()
        {
            this.webApplicationFactory = new ApiWebApplicationFactory();
            this.customerRepositoryHelper = new CustomerRepositoryHelper();
            this.databaseHelper = new DatabaseHelper();

            this.dbOptions = this.databaseHelper.GetDatabaseOptions();
            this.httpClient = this.webApplicationFactory.CreateClient();

            this.customerContext = new CustomerContext(this.dbOptions);
            this.customerRepository = new CustomerRepository(this.customerContext, this.customerRepositoryHelper);

            this.customerToBeAdded = new Customer
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "janedoe@jd.com",
                Password = "********"
            };
        }

        [Test]
        public async Task AddCustomerTest()
        {
            // act
            var response = await this.httpClient.PostAsJsonAsync<Customer>("/api/customer", this.customerToBeAdded);

            // assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task GetCustomerTest()
        {
            // arrange 
            this.customerRepository.AddCustomer(this.customerToBeAdded);

            // act 
            var response = await this.httpClient.GetAsync("/api/customer/1");
            var customerRetrieved = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());

            // assert
            Assert.AreEqual(this.customerToBeAdded.Id, customerRetrieved.Id);
        }

        [Test]
        public async Task DeleteCustomerTest()
        {
            // arrange 
            this.customerRepository.AddCustomer(this.customerToBeAdded);

            // act
            var response = await this.httpClient.DeleteAsync("api/customer/1");

            // assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        public void Dispose()
        {
            this.customerContext = null;
        }
    }
}