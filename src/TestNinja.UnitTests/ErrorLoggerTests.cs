using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_ThenSetTheLastErrorProperty()
        {
            // Arrange
            const string error = "my error";

            // Act
            _testee.Log(error);

            // Assert
            Assert.That(_testee.LastError, Is.EqualTo(error));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_WhenInvalidError_ThenThrowArgumentNullException(string error)
        {
            // Act + Assert
            Assert.That(() => _testee.Log(error), Throws.ArgumentNullException);
            //Assert.That(() => _errorLogger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Log_WhenValidError_ThenRaiseErrorLoggedEvent()
        {
            // Arrange
            var id = Guid.Empty;
            const string error = "MyError";
            _testee.ErrorLogged += (sender, args) => { id = args; };

            // Act
            _testee.Log(error);

            // Assert
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}