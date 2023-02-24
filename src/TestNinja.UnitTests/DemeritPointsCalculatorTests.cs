using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new DemeritPointsCalculator();
        }

        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_WhenSpeedIsOutOfRange_ThenThrowArgumentOutOfRangeException(int speed)
        {
            // Act + Assert
            Assert.That(() => _testee.CalculateDemeritPoints(speed),
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ThenReturnDemeritPoints(int speed, int expectedResult)
        {
            // Act
            var points = _testee.CalculateDemeritPoints(speed);

            // Assert
            Assert.That(points, Is.EqualTo(expectedResult));
        }
    }
}