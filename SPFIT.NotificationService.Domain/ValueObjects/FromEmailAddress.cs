using System.Text.RegularExpressions;

namespace SPFIT.NotificationService.Domain.ValueObjects
{
    public class FromEmailAddress
    {
        public string Value { get; set; }

        public FromEmailAddress(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            if (!IsValidFormat(value))
                throw new ArgumentException("Invalid email format.", nameof(value));

            Value = value;
        }

        private static bool IsValidFormat(string value)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return emailRegex.IsMatch(value);
        }
    }
}
