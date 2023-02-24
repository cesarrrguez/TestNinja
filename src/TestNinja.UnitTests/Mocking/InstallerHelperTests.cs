using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloaderMock;

        private InstallerHelper _testee;

        [SetUp]
        public void SetUp()
        {
            _fileDownloaderMock = new Mock<IFileDownloader>();
            _testee = new InstallerHelper(_fileDownloaderMock.Object);
        }

        [Test]
        public void DownloadInstaller_WhenDownloadFails_ThenReturnFalse()
        {
            // Arrange
            _fileDownloaderMock.Setup(x =>
                x.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            // Act
            var result = _testee.DownloadInstaller("customer", "installer");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_WhenDownloadCompletes_ThenReturnTrue()
        {
            // Arrange + Act
            var result = _testee.DownloadInstaller("customer", "installer");

            // Assert
            Assert.That(result, Is.True);
        }
    }
}