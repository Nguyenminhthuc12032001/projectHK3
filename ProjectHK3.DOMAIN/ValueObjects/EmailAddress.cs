using ProjectHK3.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace ProjectHK3.Domain.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {
        public string Value { get; }

        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private EmailAddress()
        {
            Value = string.Empty;
        }

        public EmailAddress(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Email is required");

            var normalized = value.Trim().ToLowerInvariant();

            if (!EmailRegex.IsMatch(normalized))
                throw new ArgumentException("Invalid email format.");

            Value = normalized;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
