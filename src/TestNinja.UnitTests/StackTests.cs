using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new Stack<string>();
        }

        [Test]
        public void Count_WhenEmptyStack_ThenReturnZero()
        {
            // Act + Assert
            Assert.That(_testee.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_WhenArgumentIsNull_ThenThrowArgumentNullException()
        {
            // Act + Assert
            Assert.That(() => _testee.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_WhenArgumentIsValid_ThenObjectIsAddedToTheStack()
        {
            // Act
            _testee.Push("object");

            // Assert
            Assert.That(_testee.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_WhenEmptyStack_ThenThrowInvalidOperationException()
        {
            // Act + Assert
            Assert.That(() => _testee.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_WhenStackHasSomeObjects_ThenLastObjectIsReturned()
        {
            // Arrange
            _testee.Push("object 1");
            _testee.Push("object 2");
            _testee.Push("object 3");

            // Act
            var result = _testee.Pop();

            // Assert
            Assert.That(result, Is.EqualTo("object 3"));
        }

        [Test]
        public void Pop_WhenStackHasSomeObjects_ThenRemoveObjectOnTheTop()
        {
            // Arrange
            _testee.Push("object 1");
            _testee.Push("object 2");
            _testee.Push("object 3");

            // Act
            _testee.Pop();

            // Assert
            Assert.That(_testee.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_WhenEmptyStack_ThenThrowInvalidOperationException()
        {
            // Act + Assert
            Assert.That(() => _testee.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_WhenStackHasSomeObjects_ThenLastObjectIsReturned()
        {
            // Arrange
            _testee.Push("object 1");
            _testee.Push("object 2");
            _testee.Push("object 3");

            // Act
            var result = _testee.Peek();

            // Assert
            Assert.That(result, Is.EqualTo("object 3"));
        }

        [Test]
        public void Peek_WhenStackHasSomeObjects_ThenNotRemoveObjectOnTheTop()
        {
            // Arrange
            _testee.Push("object 1");
            _testee.Push("object 2");
            _testee.Push("object 3");

            // Act
            _testee.Peek();

            // Assert
            Assert.That(_testee.Count, Is.EqualTo(3));
        }
    }
}