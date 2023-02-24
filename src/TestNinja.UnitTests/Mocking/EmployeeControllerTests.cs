using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStorageMock;

        private EmployeeController _testee;

        [SetUp]
        public void SetUp()
        {
            _employeeStorageMock = new Mock<IEmployeeStorage>();
            _testee = new EmployeeController(_employeeStorageMock.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ThenDeleteTheEmployeeFromDb()
        {
            // Arrange + Act
            _testee.DeleteEmployee(1);

            // Assert
            _employeeStorageMock.Verify(x => x.DeleteEmployee(1));
        }
    }
}