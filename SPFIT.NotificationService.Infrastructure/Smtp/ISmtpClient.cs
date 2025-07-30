using System.Net.Mail;

namespace SPFIT.NotificationService.Infrastructure.Smtp
{
    public interface ISmtpClient : IDisposable
    {
        event SendCompletedEventHandler SendCompleted;
        Task SendMailAsync(MailMessage message);
        void SendAsyncCancel();
    }

    public class SmtpClientProxy : ISmtpClient
    {
        private readonly SmtpClient _client;

        public SmtpClientProxy(SmtpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public event SendCompletedEventHandler SendCompleted
        {
            add => _client.SendCompleted += value;
            remove => _client.SendCompleted -= value;
        }

        public async Task SendMailAsync(MailMessage message) => await _client.SendMailAsync(message);
        public void SendAsyncCancel() => _client.SendAsyncCancel();
        public void Dispose() => _client.Dispose();
    }

}
