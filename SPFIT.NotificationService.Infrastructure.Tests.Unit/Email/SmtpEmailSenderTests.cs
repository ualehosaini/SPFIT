using NSubstitute;
using SPFIT.NotificationService.Domain.ValueObjects;
using SPFIT.NotificationService.Infrastructure.Smtp;
using System.Net.Mail;

namespace SPFIT.NotificationService.Infrastructure.Tests.Unit.Email
{
    [TestClass]
    public class SmtpEmailSenderTests
    {
        private const string FromEmail= "from@example.com";
        private const string ToEmail = "to@example.com";
        private const string MailSubject = "subject";
        private const string MailBody = "body";

        private ISmtpClient? _smtpClient;
        private FromEmailAddress? _fromEmailAddress;
        private MailContent? _mailContent;

        private SmtpEmailSender _sut;

        [TestInitialize]
        public void Setup()
        {
            _smtpClient = Substitute.For<ISmtpClient>();
            _fromEmailAddress = new FromEmailAddress(FromEmail);
            _mailContent = new MailContent(ToEmail, MailSubject, MailBody);

            _sut = new SmtpEmailSender(_smtpClient);
        }

        [TestMethod]
        public async Task SendEmailAsync_ValidArguments_CallsSmtpClientWithCorrectMailMessage()
        {
            // Act
            await _sut.SendEmailAsync(_fromEmailAddress, _mailContent);

            // Assert
            await _smtpClient.Received(1).SendMailAsync(Arg.Is<MailMessage>(msg =>
                            msg.From.Address == FromEmail &&
                            msg.To[0].Address == ToEmail &&
                            msg.Subject == MailSubject &&
                            msg.Body == MailBody
            ));
        }

        [TestMethod]
        public async Task SendEmailAsync_NullFromEmailAddress_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _sut.SendEmailAsync(null, _mailContent));
        }

        [TestMethod]
        public async Task SendEmailAsync_NullMailContent_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _sut.SendEmailAsync(_fromEmailAddress, null));
        }

        [TestMethod]
        public void Constructor_NullSmtpClient_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SmtpEmailSender(null));
        }
    }
}
