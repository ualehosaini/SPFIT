using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPFIT.NotificationService.Domain.Interfaces;
using SPFIT.NotificationService.Infrastructure.Configurations;
using SPFIT.NotificationService.Infrastructure.Smtp;
using System.Net;
using System.Net.Mail;

namespace SPFIT.NotificationService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSmtpClient(configuration);
            services.AddScoped<INotificationService, SmtpEmailSender>();
            return services;
        }

        public static IServiceCollection AddSmtpClient(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            var smtpConfig = new SmtpConfig();
            configuration.GetSection("AppConfig:SmtpConfig").Bind(smtpConfig);

            services.AddSingleton<ISmtpClient>(sp =>
            {
                var smtpClient = new SmtpClient
                {
                    Host = smtpConfig.Host,
                    Port = smtpConfig.Port,
                    Credentials = new NetworkCredential(smtpConfig.User, smtpConfig.Password),
                    EnableSsl = true
                };

                return new SmtpClientProxy(smtpClient);
            });

            return services;
        }
    }
}
