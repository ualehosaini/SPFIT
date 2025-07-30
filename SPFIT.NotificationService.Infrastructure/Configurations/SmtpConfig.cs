namespace SPFIT.NotificationService.Infrastructure.Configurations
{
    /// <summary>
    /// SMTP configuration settings.
    /// </summary>
    public class SmtpConfig
    {
        /// <summary>
        /// Port number to be used for SMTP communication.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Username for SMTP authentication.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password for SMTP authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Hostname of the SMTP server.
        /// </summary>
        public string Host { get; set; }
    }
}