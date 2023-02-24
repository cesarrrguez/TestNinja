using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IStorage> _storageMock;

        private OrderService _testee;

        [SetUp]
        public void SetUp()
        {
            _storageMock = new Mock<IStorage>();
            _testee = new OrderService(_storageMock.Object);
        }

        [Test]
        public void PlaceOrder_WhenCalled_ThenStoreTheOrder()
        {
            // Arrange
            var order = new Order();

            // Act
            _testee.PlaceOrder(order);

            // Assert
            _storageMock.Verify(x => x.Store(order));
        }
    }
}