using Data;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public partial class CusotmerRespositoryTests
    {
        public class GetCustomerTest : CusotmerRespositoryTests
        {
            [Test]
            public void GetCusomerTest_DatabaseDoesNotExist_RedPath()
            {
                // arrange 
                var localCustomerRepository = new CustomerRepository(this.customerContext, this.customerRepositoryHelperMock.Object);
                this.customerRepositoryHelperMock.Setup(crhMock => crhMock.DatabaseDoesNotExist(It.IsAny<CustomerContext>())).Returns(true);

                // act
                var response = localCustomerRepository.GetCustomer(12);

                // assert
                Assert.IsNull(response);
            }

            [Test]
            public void GetCustomerTest_When_Valid_CustomerId_GreenPath()
            {
                // arrange 
                var customerToBeRetrieved = this.customerToBeAdded;
                if (!this.customerRepository.ContainsCustomer(this.customerToBeAdded))
                {
                    this.customerRepository.AddCustomer(this.customerToBeAdded);
                }

                // act
                var retrievedCustomer = this.customerRepository.GetCustomer(customerToBeRetrieved.Id);

                // assert
                Assert.AreEqual(retrievedCustomer.Id, customerToBeRetrieved.Id);
            }
        }
    }
}