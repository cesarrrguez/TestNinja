using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class ProductTests
    {
        private Product _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new Product { ListPrice = 100 };
        }

        [Test]
        public void GetPrice_WhenGoldCustomer_ThenApply30PercentDiscount()
        {
            // Arrange
            var customer = new Customer { IsGold = true };

            // Act
            var result = _testee.GetPrice(customer);

            // Assert
            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void GetPrice_WhenGoldCustomer_ThenApply30PercentDiscount2()
        {
            // Arrange
            var customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsGold).Returns(true);

            // Act
            var result = _testee.GetPrice(customer.Object);

            // Assert
            Assert.That(result, Is.EqualTo(70));
        }
    }
}