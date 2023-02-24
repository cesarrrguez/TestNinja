using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Mock<IVideoRepository> _videoRepositoryMock;

        private VideoService _testee;

        [SetUp]
        public void SetUp()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _videoRepositoryMock = new Mock<IVideoRepository>();

            _testee = new VideoService(_fileReaderMock.Object, _videoRepositoryMock.Object);
        }

        [Test]
        public void ReadVideoTitle_WhenEmptyFile_ThenReturnError()
        {
            // Arrange
            _fileReaderMock.Setup(x => x.Read("video.txt")).Returns(string.Empty);

            // Act
            var result = _testee.ReadVideoTitle();

            // Assert
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WhenAllVideosAreProcessed_ThenReturnAnEmptyString()
        {
            // Arrange
            _videoRepositoryMock.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>());

            // Act
            var result = _testee.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WhenAFewUnprocessedVideos_ThenReturnAStringWithIdOfUnprocessedVideos()
        {
            // Arrange
            _videoRepositoryMock.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            // Act
            var result = _testee.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}