using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_WhenInputIsDivisibleBy3And5_ThenReturnFizzBuzz()
        {
            // Act
            var result = FizzBuzz.GetOutput(15);

            // Assert
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_WhenInputIsDivisibleOnlyBy3_ThenReturnFizz()
        {
            // Act
            var result = FizzBuzz.GetOutput(3);

            // Assert
            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_WhenInputIsDivisibleOnlyBy5_ThenReturnBuzz()
        {
            // Act
            var result = FizzBuzz.GetOutput(5);

            // Assert
            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_WhenInputIsNotDivisibleBy3Or5_ThenReturnTheSameNumber()
        {
            // Act
            var result = FizzBuzz.GetOutput(1);

            // Assert
            Assert.That(result, Is.EqualTo("1"));
        }
    }
}