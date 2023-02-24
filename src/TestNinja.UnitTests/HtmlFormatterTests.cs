using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        private HtmlFormatter _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new HtmlFormatter();
        }

        [Test]
        public void FormatAsBold_WhenCalled_ThenShouldEncloseTheStringWithStrongElement()
        {
            // Arrange
            const string content = "my content";

            // Act
            var result = _testee.FormatAsBold(content);

            // Assert
            // Specific
            Assert.That(result, Is.EqualTo($"<strong>{content}</strong>").IgnoreCase);

            // More general
            Assert.Multiple(() =>
            {
                Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
                Assert.That(result, Does.EndWith("</strong>"));
                Assert.That(result, Does.Contain(content));
            });
        }
    }
}
