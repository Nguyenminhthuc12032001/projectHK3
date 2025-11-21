using ProjectHK3.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace ProjectHK3.Domain.ValueObjects
{
    internal class PhoneNumber : ValueObject
    {
        public string? Value { get; set; }

        private static readonly Regex E164Regex =
            new Regex(@"")

        public PhoneNumber(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Phone number is required");
            }

            var cleaned = value.Replace(" ", "").Replace("-", "").Trim();
            if (!Regex.IsMatch(cleaned, @"^(0|\+84)[0-9]{9}$"))
            {
                throw new ArgumentException("Invalid phone number format");
            }

            if (cleaned.StartsWith("0"))
            {
                cleaned = "+84" + cleaned.Substring(1);
            }

            Value = cleaned;
        }
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
