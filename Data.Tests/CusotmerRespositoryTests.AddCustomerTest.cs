using NUnit.Framework;

namespace Tests
{
    public partial class CusotmerRespositoryTests
    {
        public class AddCustomerTest : CusotmerRespositoryTests
        {
            [Test]
            public void AddCustomerTest_GreenPath()
            {
                // arrange
                if (this.customerRepository.ContainsCustomer(this.customerToBeAdded))
                {
                    this.customerRepository.DeleteCustomer(this.customerToBeAdded.Id);
                }

                // act
                this.customerRepository.AddCustomer(this.customerToBeAdded);
                var customerRetrivedFromDb = this.customerRepository.GetCustomer(this.customerToBeAdded.Id);

                // assert
                Assert.AreEqual(this.customerToBeAdded.Id, customerRetrivedFromDb.Id);
            }

            [Test]
            public void AddCustomerTest_When_Customer_Already_Exists_GreenPath()
            {
                // arrange
                if (!this.customerRepository.ContainsCustomer(this.customerToBeAdded))
                {
                    this.customerRepository.AddCustomer(this.customerToBeAdded);
                }

                // act
                var response = this.customerRepository.AddCustomer(this.customerToBeAdded);

                // Assert 
                Assert.IsTrue(response);
            }

            [Test]
            public void AddCustomerTest_When_Customer_Is_Null_RedPath()
            {
                // act
                var response = this.customerRepository.AddCustomer(null);

                // assert
                Assert.IsFalse(response);
            }
        }
    }
}