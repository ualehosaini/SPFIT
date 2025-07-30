namespace SPFIT.NotificationService.Domain.ValueObjects
{
    public class MailContent
    {
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        
        public MailContent(string to, string subject, string body) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(to);
            ArgumentException.ThrowIfNullOrWhiteSpace(subject);
            ArgumentException.ThrowIfNullOrWhiteSpace(body);

            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
