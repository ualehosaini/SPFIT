using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPFIT.NotificationService.Domain.Interfaces;
using SPFIT.NotificationService.Infrastructure.Configurations;
using SPFIT.NotificationService.Infrastructure.Extensions;
using SPFIT.NotificationService.Infrastructure.Smtp;

namespace SPFIT.NotificationService.Infrastructure.Tests.Unit.Extensions
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        private IServiceCollection _services = new ServiceCollection();
        private IConfiguration? _configuration;

        [TestInitialize]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"AppConfig:SmtpConfig:Host", "smtp.test.com"},
                {"AppConfig:SmtpConfig:Port", "2525"},
                {"AppConfig:SmtpConfig:User", "testuser"},
                {"AppConfig:SmtpConfig:Password", "testpass"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [TestMethod]
        public void AddSmtpClient_Registers_ISmtpClient_AsSingleton()
        {
            // Act
            _services.AddSmtpClient(_configuration);
            var provider = _services.BuildServiceProvider();

            // Assert
            var smtpClient1 = provider.GetService<ISmtpClient>();
            var smtpClient2 = provider.GetService<ISmtpClient>();

            Assert.IsNotNull(smtpClient1);
            Assert.IsInstanceOfType(smtpClient1, typeof(SmtpClientProxy));
            AssertTheyAreSameInstance(smtpClient1, smtpClient2);

            static void AssertTheyAreSameInstance(ISmtpClient smtpClient1, ISmtpClient? smtpClient2)
                => Assert.AreSame(smtpClient1, smtpClient2);
        }

        [TestMethod]
        public void AddInfrastructureServices_Registers_Dependencies()
        {
            // Act
            _services.AddInfrastructureServices(_configuration);
            var provider = _services.BuildServiceProvider();

            // Assert
            var notificationService = provider.GetService<INotificationService>();
            var smtpClient = provider.GetService<ISmtpClient>();

            Assert.IsNotNull(notificationService);
            Assert.IsNotNull(smtpClient);
            Assert.IsInstanceOfType(notificationService, typeof(SmtpEmailSender));
            Assert.IsInstanceOfType(smtpClient, typeof(SmtpClientProxy));
        }

        [TestMethod]
        public void AddSmtpClient_Binds_SmtpConfig_From_Configuration()
        {
            // Arrange
            var smtpConfig = new SmtpConfig();
            _configuration.GetSection("AppConfig:SmtpConfig").Bind(smtpConfig);

            // Act
            _services.AddSmtpClient(_configuration);

            // Assert
            Assert.AreEqual("smtp.test.com", smtpConfig.Host);
            Assert.AreEqual(2525, smtpConfig.Port);
            Assert.AreEqual("testuser", smtpConfig.User);
            Assert.AreEqual("testpass", smtpConfig.Password);
        }
    }
}