using Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CusotmerRespositoryTests
    {
        private DbContextOptions<CustomerContext> testDbOptions;
        private CustomerRepository customerRepository;

        [SetUp]
        public void Setup()
        {
            this.testDbOptions = SqliteHelper.GetInMemoryDatabaseOptions();
        }

        [Test]
        public void CheckIfDatabaseExistsTest()
        {
            // assert
            using (var customerContext = new CustomerContext(this.testDbOptions))
            {
                Assert.IsFalse(SqliteHelper.DatabaseDoesNotExist(customerContext));
            }
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
            var customerRetrivedFromDb = new Customer();
            using (var customerContext = new CustomerContext(this.testDbOptions))
            {
                if (SqliteHelper.InMemoryDatabaseDoesNotExist(customerContext))
                {
                    Assert.Fail();
                }

                // add
                this.customerRepository = new CustomerRepository(customerContext);
                this.customerRepository.AddCustomer(customerToBeAddedToDb);
                this.customerRepository.SaveChanges();

                // retrieve
                customerRetrivedFromDb = this.customerRepository.GetCustomer(customerToBeAddedToDb.Id);
            }

            // assert
            Assert.AreEqual(customerToBeAddedToDb, customerRetrivedFromDb);
        }

        [TearDown]
        public void TearDown()
        {
            using (var customerContext = new CustomerContext(this.testDbOptions))
            {
                customerContext.Database.EnsureDeleted();
            }
        }
    }
}