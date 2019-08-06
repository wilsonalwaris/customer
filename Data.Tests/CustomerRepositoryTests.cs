using Data;
using Moq;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public partial class CusotmerRespositoryTests : IDisposable
    {
        private ICustomerRepository customerRepository;
        private ICustomerRepositoryHelper customerRepositoryHelper;
        private Mock<ICustomerRepositoryHelper> customerRepositoryHelperMock;
        private IDatabaseHelper databaseHelper;
        private CustomerContext customerContext;
        private Customer customerToBeAdded;

        [SetUp]
        public void Setup()
        {
            this.databaseHelper = new DatabaseHelper();
            this.customerRepositoryHelper = new CustomerRepositoryHelper();
            this.customerRepositoryHelperMock = new Mock<ICustomerRepositoryHelper>();

            this.customerContext = new CustomerContext(this.databaseHelper.GetDatabaseOptions());
            this.customerRepository = new CustomerRepository(this.customerContext, this.customerRepositoryHelper);
            this.customerToBeAdded= new Customer
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "janedoe@jd.com",
                Password = "********"
            };
        }

        [Test]
        public void CheckIfDatabaseExistsTest()
        {
            // assert
            Assert.IsFalse(this.customerRepositoryHelper.DatabaseDoesNotExist(this.customerContext));
        }

        public void Dispose()
        {
            this.customerContext = null;
        }
    }
}