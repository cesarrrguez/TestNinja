using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private readonly DateTime _statementDate = new DateTime(2017, 1, 1);

        private Mock<IStatementGenerator> _statementGeneratorMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IXtraMessageBox> _messageBoxMock;

        private Housekeeper _houseKeeper;
        private string _statementFileName;

        private HouseKeeperService _testee;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGeneratorMock = new Mock<IStatementGenerator>();
            _statementGeneratorMock
                .Setup(x => x.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSenderMock = new Mock<IEmailSender>();
            _messageBoxMock = new Mock<IXtraMessageBox>();

            _testee = new HouseKeeperService(
                unitOfWorkMock.Object,
                _statementGeneratorMock.Object,
                _emailSenderMock.Object,
                _messageBoxMock.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_ThenGenerateStatements()
        {
            // Act
            _testee.SendStatementEmails(_statementDate);

            // Assert
            _statementGeneratorMock.Verify(
                x => x.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhenHouseKeepersEmailIsNullOrWhiteSpace_ThenShouldNotGenerateStatement(
            string email)
        {
            // Arrange
            _houseKeeper.Email = email;

            // Act
            _testee.SendStatementEmails(_statementDate);

            // Assert
            _statementGeneratorMock.Verify(x =>
                x.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_ThenEmailTheStatement()
        {
            // Act
            _testee.SendStatementEmails(_statementDate);

            // Assert
            VerifyEmailSent();
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhenStatementFileNameIsNullOrWhiteSpace_ThenShouldNotEmailTheStatement(
            string email)
        {
            // Arrange
            _statementFileName = email;

            // Act
            _testee.SendStatementEmails(_statementDate);

            // Assert
            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_WhenEmailSendingFails_ThenDisplayAMessageBox()
        {
            // Arrange
            _emailSenderMock.Setup(x => x.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Throws<Exception>();

            // Act
            _testee.SendStatementEmails(_statementDate);

            // Assert
            _messageBoxMock.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.Ok));
        }

        private void VerifyEmailSent()
        {
            _emailSenderMock.Verify(x => x.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSenderMock.Verify(x => x.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }
    }
}