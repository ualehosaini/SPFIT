using SPFIT.NotificationService.Domain.Interfaces;
using SPFIT.NotificationService.Domain.ValueObjects;
using SPFIT.NotificationService.Infrastructure.Smtp;
using System.Net.Mail;

/// <summary>
/// SMTP implementation of the INotificationService interface for sending emails.
/// </summary>
public class SmtpEmailSender : INotificationService
{
    private readonly ISmtpClient _smtpClient;

    public SmtpEmailSender(ISmtpClient smtpClient)
    {
        _smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient));
    }

    public async Task SendEmailAsync(FromEmailAddress emailAddress ,MailContent content)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        ArgumentNullException.ThrowIfNull(content);

        var mail = new MailMessage(emailAddress.Value, content.To, content.Subject, content.Body);
        await _smtpClient.SendMailAsync(mail);
    }
}
