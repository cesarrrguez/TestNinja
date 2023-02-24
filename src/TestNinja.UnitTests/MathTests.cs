using System.Linq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _testee;

        [SetUp]
        // Called before each test
        public void SetUp()
        {
            _testee = new Math();
        }

        [TearDown]
        // Called after each test
        public void TearDown()
        {
            _testee = null;
        }

        [OneTimeSetUp]
        // Called once before all tests
        public void OneTimeSetUp()
        {
            // OneTimeSetUp
        }

        [OneTimeTearDown]
        // Called once after all tests
        public void OneTimeTearDown()
        {
            // OneTimeTearDown
        }

        [Test]
        // [Ignore("Because I wanted to!")]
        public void Add_WhenCalled_ThenReturnTheSumOfArguments()
        {
            // Act
            var result = _testee.Add(1, 2);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ThenReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            // Act
            var result = _testee.Max(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_WhenLimitIsGreaterThanZero_ThenReturnOddNumbersUpToLimit()
        {
            // Act
            var result = _testee.GetOddNumbers(5).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Empty);
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result, Does.Contain(1));
                Assert.That(result, Does.Contain(3));
                Assert.That(result, Does.Contain(5));
                Assert.That(result, Is.Ordered);
                Assert.That(result, Is.Unique);
            });
        }
    }
}