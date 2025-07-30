using SPFIT.NotificationService.Domain.ValueObjects;

namespace SPFIT.NotificationService.Domain.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Sends an email asynchronously using the provided email address and content.
        /// </summary>
        /// <param name="emailAddress">From email address</param>
        /// <param name="content">Mail content</param>
        /// <returns></returns>
        Task SendEmailAsync(FromEmailAddress emailAddress, MailContent content);
    }
}
