using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new CustomerController();
        }

        [Test]
        public void GetCustomer_WhenIdIsZero_ThenReturnNotFound()
        {
            // Act
            var result = _testee.GetCustomer(0);

            // Assert
            Assert.That(result, Is.TypeOf<NotFound>());

            // NotFound or one of its derivatives
            //Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_WhenIdIsNotZero_ThenReturnOk()
        {
            // Act
            var result = _testee.GetCustomer(1);

            // Assert
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}