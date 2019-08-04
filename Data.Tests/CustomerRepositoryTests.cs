using Data;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class CusotmerRespositoryTests : IDisposable
    {
        private ICustomerRepository customerRepository;
        private ICustomerRepositoryHelper customerRepositoryHelper;
        private IDatabaseHelper databaseHelper;
        private CustomerContext customerContext;

        [SetUp]
        public void Setup()
        {
            this.databaseHelper = new DatabaseHelper();
            this.customerRepositoryHelper = new CustomerRepositoryHelper();
            this.customerContext = new CustomerContext(this.databaseHelper.GetDatabaseOptions());
            this.customerRepository = new CustomerRepository(this.customerContext, this.customerRepositoryHelper);
        }

        [Test]
        public void CheckIfDatabaseExistsTest()
        {
            // assert
            Assert.IsFalse(this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext));
        }

        [Test]
        public void CreateCustomerTest()
        {
            // arrange
            var customerToBeAddedToDb = new Customer
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "janedoe@jd.com",
                Password = "********"
            };

            // act
            if (this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext))
            {
                Assert.Fail();
            }
            this.customerRepository.AddCustomer(customerToBeAddedToDb);
            var customerRetrivedFromDb = this.customerRepository.GetCustomer(customerToBeAddedToDb.Id);

            // assert
            Assert.AreEqual(customerToBeAddedToDb.Id, customerRetrivedFromDb.Id);
        }

        public void Dispose()
        {
            this.customerContext = null;
        }
    }
}